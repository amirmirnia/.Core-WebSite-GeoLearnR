using Data.TMU.Model.Gallery;
using Data.TMU.Model.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface IGallery
    {
        ListGalleryViewModel GetAllGallery(int pageid=1,string filtertitel=null,string tag=null, int take=0);

        #region FileNews
        List<FileGallery> GetImageGallery(int IdGallery);
        bool GalleryIsImage(int IdGallery);
        void SetFirstImage(int IDFiledGallery,int IdGallery);
        FileGallery FindImageGallery(int IDFiledGallery);


        #endregion


    }
}
