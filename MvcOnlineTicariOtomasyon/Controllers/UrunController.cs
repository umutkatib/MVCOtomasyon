using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        private Context _context;

        public UrunController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string p)
        {
            var urunler = _context.Uruns.Include(x => x.Kategori).Where(x => x.UrunDurum == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }

        [HttpGet]
        public IActionResult UrunEkle()
        {
            List<SelectListItem> deger = (from x in _context.Kategoris.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.KategoriAd,
                                              Value = x.KategoriID.ToString()
                                          }).ToList();

            ViewBag.uekle = deger;
            return View();
        }
        [HttpPost]
        public IActionResult UrunEkle(Urun u)
        {
            _context.Uruns.Add(u);
            u.UrunDurum = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UrunSil(int id)
        {
            var deger = _context.Uruns.Find(id);
            deger.UrunDurum = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger = (from x in _context.Kategoris.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.KategoriAd,
                                                Value = x.KategoriID.ToString()
                                            }).ToList();
            ViewBag.uguncelle = deger;

            var urunDeger = _context.Uruns.Find(id);
            return View(urunDeger);
        }
        [HttpPost]
        public IActionResult UrunGuncelle(Urun u)
        {
            var urun = _context.Uruns.Find(u.UrunID);
            urun.UrunAlisFiyat = u.UrunAlisFiyat;
            urun.UrunSatisFiyat = u.UrunSatisFiyat;
            urun.UrunDurum = u.UrunDurum;
            urun.UrunMarka = u.UrunMarka;
            urun.UrunStok = u.UrunStok;
            urun.UrunAd = u.UrunAd;
            urun.UrunGorsel = u.UrunGorsel;
            urun.KategoriID = u.KategoriID;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UrunListesi()
        {
            var degerler = _context.Uruns.Where(x => x.UrunDurum == true).Include(x => x.Kategori).ToList();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in _context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urun = _context.Uruns.Find(id);
            ViewBag.dgr2 = urun.UrunID;
            ViewBag.dgr3 = urun.UrunSatisFiyat;
            return View();
        }
        [HttpPost]
        public IActionResult SatisYap(SatisHareket s)
        {
            s.SatisHareketTarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            _context.SatisHarekets.Add(s);
            _context.SaveChanges();
            return RedirectToAction("Index", "Satis");
        }
    }
}
