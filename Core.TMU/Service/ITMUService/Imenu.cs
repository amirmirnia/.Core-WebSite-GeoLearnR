using Data.TMU.Model.Menu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface Imenu
    {
        ListMenuViewModel GetAllMenu(int pageid = 1, string filtertitel = null, int take = 0);

        List<SelectListItem> GetSelect1();
        List<SelectListItem> GetSelect2(int id);
        string TitelMenu(int id);
        bool IsParent(int id);


    }
}
