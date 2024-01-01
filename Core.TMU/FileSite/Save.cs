using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;

namespace Core.TMU.FileSite
{
    public class Save
    {
        public async Task<Tuple<bool, string>> SaveFileasync(Task<IFormFile> filee, string filePath,string namefile=null)
        {
            var file = await filee;
            if (file != null && file.Length > 0)
            {
                string pathstring;
                string NewName = Guid.NewGuid().ToString().Replace("-", "") + "" + Path.GetExtension(file.FileName);
                if (namefile!=null)
                {
                    NewName = namefile;
                }
                pathstring = Path.Combine(Directory.GetCurrentDirectory(), filePath,
                    NewName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }

                return new Tuple<bool, string>(true, NewName);
            }

            return new Tuple<bool, string>(false, "non");
        }
        public static Tuple<bool, string> SaveFile(IFormFile file, string path, string namefile = null)
        {

            if (file != null)
            {
                string pathstring;
                string NewName = "";
                if (namefile != null)
                {
                    NewName = namefile;
                }
                else
                {
                    NewName = Guid.NewGuid().ToString().Replace("-", "") + "" + Path.GetExtension(file.FileName);
                }
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
                return new Tuple<bool, string>(false, "non");
            }
        }
     



        

    }
}

