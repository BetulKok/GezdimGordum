using GezdimGördüm.Attiributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GezdimGördüm.Models
{
    public class PaylasimEkleViewModel
    {
        public string Aciklama { get; set; }

        //IFormFile: Dosyanın adı byteları barındırıyor... Resim Yolunu biz üreteceğiz benzersiz. Aynı isimle olursa ve iki resim yüklenirse eskisi gider...

        [Required(ErrorMessage ="Bu alan bos bırakılamaz!")]
        [GecerliResim(MaksimumDostaBoyutu =1000)]
        public IFormFile Resim { get; set; }
    }
}
