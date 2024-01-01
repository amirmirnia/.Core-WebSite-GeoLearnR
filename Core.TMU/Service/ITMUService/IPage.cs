using Data.TMU.Model.Image;
using Data.TMU.Model.News;
using Data.TMU.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface IPage
    {
        ListPageViewModel GetAllPage(int pageid=1,string filtertitel=null,string tag=null, int take=0);

        #region FilePage
        List<FilePage> GetImagePage(int IdPage);
        bool PageIsImage(int IdPage);
        void SetFirstImage(int IDFiledPage, int IdPage);
        FilePage FindImagePage(int IDFiledPage);


        #endregion
        ListImagePageViewModel GetAllImagePage(int pageid = 1, string filtertitel = null, string tag = null, int take = 0);
        ImagePage FindImageSite(int IDImagePage);


    }
}
