using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Img
{
    public class SaveImage
    {
        public static Tuple<bool, string> SaveImageProfile(IFormFile file, string OldName,string path)
        {
            if (file != null)
            {
                string pathstring;
                if (OldName != "DefultAvatar.jpg")
                {
                    pathstring = Path.Combine(Directory.GetCurrentDirectory(), path, OldName);
                    if (File.Exists(pathstring))
                    {
                        File.Delete(pathstring);
                    }
                }


                string NewName = Guid.NewGuid().ToString().Replace("-", "") +"" + Path.GetExtension(file.FileName);
                pathstring = Path.Combine(Directory.GetCurrentDirectory(), path,
                    NewName);
                using (var Stream = new FileStream(pathstring, FileMode.Create))
                {
                    file.CopyTo(Stream);
                }
                return new Tuple<bool, string>(true, NewName);
            }
            else
            {
                return new Tuple<bool, string>(false, "DefultAvatar.jpg");
            }
        }

        public static Tuple<bool, string> SaveImageNews(IFormFile file, string path)
        {
            if (file != null)
            {
                string pathstring;
                string NewName = Guid.NewGuid().ToString().Replace("-", "") + "" + Path.GetExtension(file.FileName);
                pathstring = Path.Combine(Directory.GetCurrentDirectory(), path,
                    NewName);
                using (var Stream = new FileStream(pathstring, FileMode.Create))
                {
                    file.CopyTo(Stream);
                }
                return new Tuple<bool, string>(true, NewName);
            }
            else
            {
                return new Tuple<bool, string>(false, "DefultImage.jpg");
            }
        }

    }
}
