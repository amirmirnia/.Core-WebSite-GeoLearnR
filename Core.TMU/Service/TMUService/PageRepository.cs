using Core.TMU.Img;
using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model.Image;
using Data.TMU.Model.News;
using Data.TMU.Model.Page;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class PageRepository : IPage
    {
        
        private ContextTMU _db;
        private IGeneric<FileNews> _gFileNews;
        private IGeneric<ImagePage> _gImagePage;
        public PageRepository(ContextTMU db, IGeneric<FileNews> gFileNews)
        {
            _db = db;
            _gFileNews = gFileNews;
        }

        public FilePage FindImagePage(int IDFiledPage)
        {
            return _db.FilePages.Find(IDFiledPage);
        }

        public ImagePage FindImageSite(int IDImagePage)
        {
            return _db.imagePages.Find(IDImagePage);
        }

        public ListImagePageViewModel GetAllImagePage(int pageid = 1, string filtertitel = null, string tag = null, int take = 0)
        {
            IQueryable<ImagePage> result = _db.imagePages;
            if (!string.IsNullOrEmpty(tag) || !string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.Titel.Contains(filtertitel) || p.Tags.Contains(tag));
            }
            int skip = (pageid - 1) * take;

            var count = result.Select(n => new ImagePage
            {
                Description = n.Description,
                DetaPage = n.DetaPage,
                Id = n.Id,
                Tags = n.Tags,
                Titel = n.Titel,
                author = n.author,
                CountView = n.CountView,
                PathFile = n.PathFile

            }).Count();
            var Listimagepage = result.Select(n => new ImagePage
            {
                Description = n.Description,
                DetaPage = n.DetaPage,
                Id = n.Id,
                Tags = n.Tags,
                Titel = n.Titel,
                author = n.author,
                CountView = n.CountView,
                PathFile = n.PathFile



            }).OrderByDescending(p => p.DetaPage).Skip(skip).Take(take).ToList();
            return new ListImagePageViewModel()
            {
                ListimagePages = Listimagepage,
                CountPage = (count / take) + 1,
                IdPage = pageid
            };
        }

        public ListPageViewModel GetAllPage(int pageid = 1, string filtertitel = null, string tag = null, int take = 0)
        {
            IQueryable<Page> result = _db.Pages;
            if (!string.IsNullOrEmpty(tag) || !string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.TitelPage.Contains(filtertitel) || p.tag.Contains(tag));
            }  
            int skip = (pageid - 1) * take;

            var count = result.Include(p => p.FilePages).Select(n=>new CategoryPage
            {
                DescriptionPage = n.Description,
                DetaPage = n.DetaNews,
                IdPage = n.Id,
                Tags = n.tag,
                TitelPage = n.TitelPage,
                author = n.author,
                CountView = n.CountView,
                image = n.FilePages.Where(p => p.IsFirst == true).Single().PathFile

            }).Count();
            var listpage = result.Include(p=>p.FilePages).Select(n => new CategoryPage
            {
                DescriptionPage = n.Description,
                DetaPage = n.DetaNews,
                IdPage = n.Id,
                Tags = n.tag,
                TitelPage = n.TitelPage,
                author=n.author,
                CountView=n.CountView,
                image=n.FilePages.Where(p => p.IsFirst == true).Single().PathFile
                


            }).OrderByDescending(p => p.DetaPage).Skip(skip).Take(take).ToList();
            return new ListPageViewModel()
            {
                ListPage = listpage,
                CountPage = (count / take)+1,
                IdPage = pageid
            };
        }

        public List<FilePage> GetImagePage(int IdPage)
        {
            return _db.FilePages.Where(p => p.IdP == IdPage).ToList();
        }

        public bool PageIsImage(int IdPage)
        {

            if (GetImagePage(IdPage).Count != 0)
            {
                return true;
            }
            return false;
        }

        public void SetFirstImage(int IDFiledPage, int IdPage)
        {
            if (_db.FileNews.FirstOrDefault(i => i.IdN == IdPage && i.IsFirst == true) != null)
            {
                _db.FileNews.First(i => i.IdN == IdPage && i.IsFirst == true).IsFirst = false;

            }
            FindImagePage(IDFiledPage).IsFirst = true;
            _db.SaveChanges();
        }


    }
}
