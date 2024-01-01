using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model.Menu;
using Data.TMU.Model.Nav;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class NavbarRepository : INavbar
    {
        private ContextTMU _db;
        public NavbarRepository(ContextTMU db)
        {
            _db = db;
        }

        public ListNavbarViewModel GetAllNavbar(int pageid = 1, string filtertitel = null, int take = 0, string selectnavbar = "خبری")
        {
            IQueryable<Navbar> result = _db.Navbars;
            if (!string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.TitelNav.Contains(filtertitel));
            }

            result = result.Where(p => p.selectnavbar.Contains(selectnavbar));
            int skip = (pageid - 1) * take;
            var count = result.Count();
            return new ListNavbarViewModel()
            {
                ListNavbar = result.OrderBy(p => p.Rank).Skip(skip).Take(take).ToList(),
                CountPage = (count / take) + 1,
                IdPage = pageid
            };
        }
        public List<SelectListItem> GetSelect1(string? selectnavbar="خبری")
        {
            return _db.Navbars.Where(p => p.ParentId == 0 && p.selectnavbar== selectnavbar).Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.TitelNav
            }).ToList();
        }

        public List<SelectListItem> GetSelect2(int id)
        {
            return _db.Navbars.Where(p => p.ParentId == id).Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.TitelNav
            }).ToList();
        }

        public List<Navbar> GetSubNavbar(int Id)
        {
            return _db.Navbars.Where(p => p.ParentId == Id).OrderBy(p => p.Rank).ToList();
        }

        public bool IsParent(int id)
        {

            if (_db.Navbars.FirstOrDefault(p => p.ParentId == id) != null)
            {
                return true;
            }

            return false;
        }

        public string TitelNavbar(int id)
        {
            return _db.Navbars.Find(id).TitelNav;
        }
    }
}
