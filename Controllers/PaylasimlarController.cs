using GezdimGördüm.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GezdimGördüm.Controllers
{
    public class PaylasimlarController : Controller
    {
        private readonly UygulamaDbContext _db;
        public IWebHostEnvironment _env { get; }

        //.netin kendi yüklendiği dizini getirebilme teknolojisi var (IWebHostEnvironment)
        public PaylasimlarController(UygulamaDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        

        public IActionResult Yeni()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(PaylasimEkleViewModel vm)
        {
            //Artık bu kısmı eklediğim attribute ile kontrol ediyoruz.
            //gelen verinin geçerli formatta vs gelip gelmediğini kontrol ediyoruz;
            //veri geçerliliği kontrolu;
            //if (vm.Resim != null && !vm.Resim.ContentType.StartsWith("image/"))
            //{
            //    ModelState.AddModelError("Resim", "Lütfen geçerli bir resim dosyası yükleyiniz...");
            //}
            //else if (vm.Resim != null && vm.Resim.Length > 1 * 1024 * 1024)
            //{
            //    ModelState.AddModelError("Resim", "Maksimum dosya boyutu 1MB olmalıdır...");
            //}

            if (ModelState.IsValid)
            {
                //öncelikle yüklenecek dosyanın uzantısını elde etmeliyiz.

                string uzanti = Path.GetExtension(vm.Resim.FileName);
                string dosyaAd = Guid.NewGuid() + uzanti;
                //kaydetme yolunda \ ile ilgili hata olmaması için kendisi yolu birleştiriyor- daha profesyonel
                string kaydetmeYolu = Path.Combine(_env.WebRootPath,"img",  dosyaAd); 

                using (var stream = System.IO.File.Create(kaydetmeYolu))
                {
                    vm.Resim.CopyTo(stream);
                }
                _db.Paylasimlar.Add(new Paylasim()
                {
                    Aciklama = vm.Aciklama,
                    ResimYolu = dosyaAd

                });
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Sil(int id)
        {
            var paylasim = _db.Paylasimlar.Find(id);
            if (paylasim==null)
            {
                return NotFound();
            }
            //paylaşımı silmeden önce dosyası varsa onu silmemiz lazım.Kontrol edecek dosya yolunu bulmak için;
            string silmeYolu = Path.Combine(_env.WebRootPath, "img", paylasim.ResimYolu);

            if (System.IO.File.Exists(silmeYolu))
            {
                System.IO.File.Delete(silmeYolu);
            }
            _db.Paylasimlar.Remove(paylasim);
            _db.SaveChanges();

            return Ok();
        }
    }
}
