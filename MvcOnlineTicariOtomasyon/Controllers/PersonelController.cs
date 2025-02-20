using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        private Context _context;
        private readonly IWebHostEnvironment _env;

        public PersonelController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var degerler = _context.Personels.Include(x => x.Departman).ToList();
            return View(degerler);
        }

        [HttpGet] 
        public IActionResult PersonelEkle()
        {
            List<SelectListItem> deger = (from x in  _context.Departmans.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.DepartmanAd,
                                              Value = x.DepartmanID.ToString()
                                          }).ToList();
            ViewBag.dekle = deger;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PersonelEkle(Personel p, IFormFile file)
        {
            string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            if(!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(uploadsFolder, fileName);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            p.PersonelGorsel = "/images/" + fileName;
            _context.Personels.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger = (from x in _context.Departmans.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.DepartmanAd,
                                              Value = x.DepartmanID.ToString()
                                          }).ToList();
            ViewBag.pguncelle = deger;

            var personel = _context.Personels.Find(id);
            return View(personel);
        }
        public IActionResult PersonelGuncelle(Personel p)
        {
            var personel = _context.Personels.Find(p.PersonelID);
            personel.PersonelAd = p.PersonelAd;
            personel.PersonelSoyad = p.PersonelSoyad;
            personel.PersonelHakkinda = p.PersonelHakkinda;
            personel.PersonelUlke = p.PersonelUlke;
            personel.PersonelTelefon = p.PersonelTelefon;
            personel.PersonelAdres = p.PersonelAdres;
            personel.PersonelGorsel = p.PersonelGorsel;
            personel.DepartmanID = p.DepartmanID;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PersonelListe()
        {
            var personel = _context.Personels.ToList();
            return View(personel);
        }
    }
}
