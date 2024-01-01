using Core.TMU.Img;
using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model.News;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class NewsRepository : INews
    {
        private ContextTMU _db;
        private IGeneric<FileNews> _gFileNews;
        public NewsRepository(ContextTMU db, IGeneric<FileNews> gFileNews)
        {
            _db = db;
            _gFileNews = gFileNews;
        }

        public FileNews FindImageNews(int IDFiledNews)
        {
            return _db.FileNews.Find(IDFiledNews);
        }

        public ListNewsViewModel GetAllNews(int pageid = 1, string filtertitel = null, string tag = null, int take = 0, bool Search = false, string idcode = null)
        {
            IQueryable<News> result = _db.News;
            if (!string.IsNullOrEmpty(tag) || !string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.TitelNews.Contains(filtertitel) || p.Tags.Contains(tag));
            }
            if (!string.IsNullOrEmpty(idcode))
            {
                result = result.Where(p => p.author.Contains(idcode));
            }
            result = result.Where(p => p.IsSearch == Search);
           


            int skip = (pageid - 1) * take;

            var count = result.Include(n=>n.FileNews).Select(n=>new CategoryNews
            {
                author=n.author,
                Mark=n.Mark,
                subjectNews=n.SubjectNews,
                CountView=n.CountView,
                DescriptionNews=n.DescriptionNews,
                DetaNews=n.DetaNews,
                FileName=n.FileName,
                IdNews=n.IdNews,
                Tags=n.Tags,
                TitelNews=n.TitelNews,
                image=n.FileNews.Where(p=>p.IsFirst==true).Single().PathFile
                 
            }).Count();
            var listnews = result.Include(n => n.FileNews).Select(n => new CategoryNews
            {
                author = n.author,
                CountView = n.CountView,
                Mark = n.Mark,
                subjectNews = n.SubjectNews,
                DescriptionNews = n.DescriptionNews,
                DetaNews = n.DetaNews,
                FileName = n.FileName,
                IdNews = n.IdNews,
                Tags = n.Tags,
                TitelNews = n.TitelNews,
                image = n.FileNews.Where(p => p.IsFirst == true).Single().PathFile

            }).OrderByDescending(p => p.DetaNews).Skip(skip).Take(take).ToList();
            return new ListNewsViewModel()
            {
                ListNews = listnews,
                CountPage = (count / take) + 1,
                IdPage = pageid
            };
        }

        public FileNews GetImageFirstnews(int idnews)
        {
            return _db.FileNews.Where(p=>p.IdN==idnews && p.IsFirst==true).Single();
        }

        public List<FileNews> GetImageNews(int IdNews)
        {
            return _db.FileNews.Where(p => p.IdN == IdNews).ToList();
        }

       

        public bool NewsIsImage(int IdNews)
        {
            
            if (GetImageNews(IdNews).Count!=0)
            {
                return true;
            }
            return false;
        }

        public void SetFirstImage(int IDFiledNews,int IdNews)
        {
            if (_db.FileNews.FirstOrDefault(i =>i.IdN==IdNews && i.IsFirst == true)!=null)
            {
                _db.FileNews.First(i => i.IdN == IdNews && i.IsFirst == true).IsFirst = false;

            }
            FindImageNews(IDFiledNews).IsFirst = true;
            _db.SaveChanges();
        }


    }
}
