using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class SliderRepository : ISlider
    {
        private ContextTMU _db;
        public SliderRepository(ContextTMU db)
        {
            _db = db;
        }
        public ListSliderViewModel GetAllSlider(int pageid = 1, string filtertitel = null, int take = 0)
        {
            IQueryable<Slider> result = _db.Sliders;
            if (!string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.FileName.Contains(filtertitel) );
            }

            int skip = (pageid - 1) * take;

            var count = result.Count();
            return new ListSliderViewModel()
            {
                ListSlider = result.OrderBy(p => p.DetaSlider).Skip(skip).Take(take).ToList(),
                CountPage = count / take,
                IdPage = pageid
            };
        }
    }
}
