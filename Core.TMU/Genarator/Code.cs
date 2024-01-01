using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Genarator
{
    public class Code
    {
        public static string UniqCode()
        {
           return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
