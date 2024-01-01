using Data.TMU.Model.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface INews
    {
        ListNewsViewModel GetAllNews(int pageid=1,string filtertitel=null,string tag=null, int take=0,bool Search=false,string idcode=null);
        FileNews GetImageFirstnews(int idnews);
        #region FileNews
        List<FileNews> GetImageNews(int IdNews);
        bool NewsIsImage(int IdNews);
        void SetFirstImage(int IDFiledNews, int IdNews);
        FileNews FindImageNews(int IDFiledNews);


        #endregion


    }
}
