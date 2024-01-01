using Data.TMU.Model.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface ISlider
    {

        ListSliderViewModel GetAllSlider(int pageid = 1, string filtertitel = null, int take = 0);



    }
}
