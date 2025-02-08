using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        private Context _context;

        public SatisController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var satislar = _context.SatisHarekets.Include(x => x.Urun).Include(x => x.Personel).Include(x => x.Cari).ToList();
            return View(satislar);
        }

        [HttpGet]
        public IActionResult SatisYap()
        {
            List<SelectListItem> deger1 = (from x in _context.Uruns.Where(x => x.UrunDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from x in _context.Caris.Where(x => x.CariDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariId.ToString(),
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from x in _context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString(),
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public IActionResult SatisYap(SatisHareket s)
        {
            s.SatisHareketTarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            s.SatisHareketToplamTutar = s.SatisHareketAdet * s.SatisHareketFiyat;
            _context.SatisHarekets.Add(s);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SatisGetir(int id)
        {
            var satis = _context.SatisHarekets.Find(id);

            List<SelectListItem> deger1 = (from x in _context.Uruns.Where(x => x.UrunDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from x in _context.Caris.Where(x => x.CariDurum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariId.ToString(),
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from x in _context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString(),
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            return View(satis);
        }

        public IActionResult SatisGuncelle(SatisHareket s)
        {
            var satis = _context.SatisHarekets.Find(s.SatisHareketID);
            satis.CariID = s.CariID;
            satis.SatisHareketAdet = s.SatisHareketAdet;
            satis.SatisHareketFiyat = s.SatisHareketFiyat;
            satis.PersonelID = s.PersonelID;
            satis.SatisHareketTarih = s.SatisHareketTarih;
            satis.SatisHareketToplamTutar = s.SatisHareketToplamTutar;
            satis.UrunID = s.UrunID;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SatisDetay(int id)
        {
            var satislar = _context.SatisHarekets.Include(x => x.Urun).Include(x => x.Personel).Include(x => x.Cari).Where(x => x.SatisHareketID == id).ToList();
            return View(satislar);
        }
    }
}
