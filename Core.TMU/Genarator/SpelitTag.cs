using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Genarator
{
    public class SpelitTag
    {
        public static string[] tag(string tag)
        {
            string [] splittag = tag.Split('-','_','/');
            return splittag;
            
        }
        
    }
}
