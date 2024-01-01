using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Core.TMU.Security
{
    public static class  ImageValidator
    {
        public static bool Isimage(this IFormFile file)
        {
            try
            {
                var img = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
