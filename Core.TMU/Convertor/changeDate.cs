using Core.TMU.Genarator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Convertor
{
    public class changeDate
    {
        public static string Date(string date, string day)
        {
            var datefirst=SpelitTag.tag(date);
            datefirst[2] = day;
            var datefinal = "";
            for (int i = 0; i < 3; i++)
            {
                if (i==0)
                {
                    datefinal+=datefirst[0];
                }
                else
                {
                    datefinal+="/"+datefirst[i];
                }
                
            }
            return datefinal;
        }
    }
}
