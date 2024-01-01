using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Core.TMU.Convertor
{
    public static class ConvertTime
    {
        public static string ToTime(this DateTime time)
        {
            PersianCalendar PC = new PersianCalendar();
            return PC.GetYear(time) + "/" + PC.GetMonth(time).ToString("00") + "/" + PC.GetDayOfMonth(time).ToString("00");
        }


        public static string ToMiladi(this DateTime time)
        {
            GregorianCalendar pc = new GregorianCalendar();
            return pc.GetYear(time) + "/" + pc.GetMonth(time) + "/" + pc.GetDayOfMonth(time);
        }
        public static string difference(DateTime firsttime, DateTime secondtime)
        {
            TimeSpan diff = secondtime.Subtract(firsttime);
            TimeSpan diff1 = secondtime - firsttime;

            String diff2 = (secondtime - firsttime).TotalDays.ToString();

            return diff2;
        }
        public static int Compare(DateTime firsttime, DateTime secondtime)
        {

            int temp = DateTime.Compare(firsttime, secondtime);
            if (temp == 0)
            {
                return 0;
            }

            else if (temp == 1) //بزرگ تر از firsttime
            {
                return 1;
            }

            else if (temp == -1) //کوچک تر از secondtime
            {
                return -1;
            }

            return 2;

        }


    }
}
