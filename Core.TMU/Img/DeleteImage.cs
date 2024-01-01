using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Img
{
    public  class DeleteImage
    {
        public static bool Deleteimage(string FileName, string path)
        {
            if (path != null && FileName != null)
            {
                string pathstring;
                if (FileName != "DefultImage.jpg")
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
