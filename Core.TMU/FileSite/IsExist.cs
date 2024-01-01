using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.FileSite
{
    public class IsExist
    {
        public static bool ExistFile(string FileName, string path)
        {

            if (path != null && FileName != null)
            {
                string pathstring;
                if (FileName != "non")
                {
                    pathstring = Path.Combine(Directory.GetCurrentDirectory(), path, FileName);
                    if (File.Exists(pathstring))
                    {
                        return true;
                    }

                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
