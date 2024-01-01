using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model.Menu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class MenuRepository : Imenu
    {
        private ContextTMU _db;
        public MenuRepository(ContextTMU db)
        {
            _db = db;
        }

        public ListMenuViewModel GetAllMenu(int pageid = 1, string filtertitel = null, int take = 0)
        {
            IQueryable<Menu> result = _db.Menus;
            if (!string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.NameMenu.Contains(filtertitel));
            }


            int skip = (pageid - 1) * take;

            var count = result.Count();
            return new ListMenuViewModel()
            {
                LidtMenus = result.OrderBy(p => p.Id).Skip(skip).Take(take).ToList(),
                CountPage = (count / take) + 1,
                IdPage = pageid
            };
        }

       
        public List<SelectListItem> GetSelect1()
        {
            return _db.Menus.Where(p => p.ParentId == 0).Select(p=>new SelectListItem()
            {
               Value= p.Id.ToString(),
               Text=p.NameMenu
            }).ToList();
        }

        public List<SelectListItem> GetSelect2(int id)
        {
            return _db.Menus.Where(p => p.ParentId == id).Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.NameMenu
            }).ToList();
        }

        public bool IsParent(int id)
        {
           
            if (_db.Menus.FirstOrDefault(p=>p.ParentId==id)!=null)
            {
                return true;
            }

            return false;
        }

        public string TitelMenu(int id)
        {
            return _db.Menus.Find(id).NameMenu;
        }
    }
}
