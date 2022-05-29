using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GezdimGördüm.Attiributes
{
    public class GecerliResimAttribute : ValidationAttribute
    {

        // kilobyte türünden yüklenebilecek maksimum dosya boyutu default: 1024
        public int MaksimumDostaBoyutu { get; set; } = 1024;

        public override bool IsValid(object value)
        {
            IFormFile formFile = value as IFormFile;
            if (formFile==null)
            {
                return true;
            }
            if (!formFile.ContentType.StartsWith("image/"))
            {
                ErrorMessage= "Geçersiz resim dosyası";
                return false;
            }
            else if (formFile.Length>MaksimumDostaBoyutu*1024)
            {
                ErrorMessage = $"izin verilen dosya boyutu : {MaksimumDostaBoyutu}kb.";
                return false;
            }
            return true;
        }
    }
}
