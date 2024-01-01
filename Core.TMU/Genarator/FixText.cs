using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Genarator
{
    public static class FixText
    {
        public static string FixEmail(this string email)
        {
            return email.Trim().ToString();
        }
    }
}
