using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.FileSite
{
    public class Delete
    {
        public static bool DeleteFile(string FileName, string path)
        {

            if (path != null && FileName != null)
            {
                string pathstring;
                if (FileName != "non")
                {
                    pathstring = Path.Combine(Directory.GetCurrentDirectory(), path, FileName);
                    if (File.Exists(pathstring))
                    {
                        File.Delete(pathstring);
                    }

                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
