using Core.TMU.Img;
using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model.Gallery;
using Data.TMU.Model.News;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class GalleryRepository : IGallery
    {
        private ContextTMU _db;
        private IGeneric<FileGallery> _gFileGallery;
        public GalleryRepository(ContextTMU db, IGeneric<FileGallery> gFileGallery)
        {
            _db = db;
            _gFileGallery = gFileGallery;
        }

        public FileGallery FindImageGallery(int IDFiledGallery)
        {
            return _db.FileGalleries.Find(IDFiledGallery);
        }

        public ListGalleryViewModel GetAllGallery(int pageid = 1, string filtertitel = null, string tag = null, int take = 0)
        {
            IQueryable<Gallery> result = _db.Galleries;
            if (!string.IsNullOrEmpty(tag) || !string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.Titel.Contains(filtertitel) || p.Tag.Contains(tag));
            }
           


            int skip = (pageid - 1) * take;

            var count = result.Include(n=>n.FileGalleries).Select(n=>new CategoryGallery
            {
                author=n.author,
                View=n.View,
                Description=n.Description,
                DateGallery=n.DateGallery,
                IdGallery=n.Id,
                Tag=n.Tag,
                Titel=n.Titel,
                image=n.FileGalleries.Where(p=>p.IsFirst==true).Single().PathFile
                 
            }).Count();
            var listGallery = result.Include(n => n.FileGalleries).Select(n => new CategoryGallery
            {
                author = n.author,
                View = n.View,
                Description = n.Description,
                DateGallery = n.DateGallery,
                IdGallery = n.Id,
                Tag = n.Tag,
                Titel = n.Titel,
                image = n.FileGalleries.Where(p => p.IsFirst == true).Single().PathFile

            }).OrderByDescending(p => p.DateGallery).Skip(skip).Take(take).ToList();
            return new ListGalleryViewModel()
            {
                ListGallery = listGallery,
                CountPage = count / take,
                IdPage = pageid
            };
        }

        public List<FileGallery> GetImageGallery(int IdGallery)
        {
            return _db.FileGalleries.Where(p => p.IdG == IdGallery).ToList();
        }

        public bool GalleryIsImage(int IdGallery)
        {
            
            if (GetImageGallery(IdGallery).Count!=0)
            {
                return true;
            }
            return false;
        }

        public void SetFirstImage(int IDFiledGallery,int IdGallery)
        {
            if (_db.FileGalleries.FirstOrDefault(i =>i.IdG== IdGallery && i.IsFirst == true)!=null)
            {
                _db.FileGalleries.First(i => i.IdG == IdGallery && i.IsFirst == true).IsFirst = false;
                _db.SaveChanges();
            }
            FindImageGallery(IDFiledGallery).IsFirst = true;
            _db.SaveChanges();
        }


    }
}
