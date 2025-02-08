using Microsoft.AspNetCore.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        private Context _context;

        public IstatistikController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var deger1 = _context.Caris.Where(x => x.CariDurum == true).Count().ToString();
            ViewBag.d1 = deger1;

            var deger2 = _context.Uruns.Where(x => x.UrunDurum == true).Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 = _context.Personels.Count().ToString();
            ViewBag.d3 = deger3;

            var deger4 = _context.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;


            var deger5 = _context.Uruns.Where(x => x.UrunDurum == true).Sum(x => x.UrunStok);
            ViewBag.d5 = deger5;

            var deger6 = (from x in _context.Uruns select x.UrunMarka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;

            var deger7 = _context.Uruns.Where(x => x.UrunDurum == true).Count(x => x.UrunStok <= 20);
            ViewBag.d7 = deger7;

            var deger8 = (from x in _context.Uruns orderby x.UrunSatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;


            var deger9 = (from x in _context.Uruns orderby x.UrunSatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;

            var deger10 = _context.Uruns.Where(x => x.UrunDurum == true).Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10 = deger10;

            var deger11 = _context.Uruns.Where(x => x.UrunDurum == true).Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;

            var deger12 = _context.Uruns.GroupBy(x => x.UrunMarka).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();
            ViewBag.d12 = deger12;

            var deger13 = _context.Uruns.Where(x => x.UrunID == (_context.SatisHarekets.GroupBy(x => x.UrunID).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault())).Select(x => x.UrunMarka).FirstOrDefault();
            ViewBag.d13 = deger13;

            var deger14 = _context.SatisHarekets.Sum(x => x.SatisHareketToplamTutar);
            ViewBag.d14 = deger14;

            DateTime dt = DateTime.Today;
            var deger15 = _context.SatisHarekets.Count(x => x.SatisHareketTarih == dt).ToString();
            ViewBag.d15 = deger15;

            var deger16 = _context.SatisHarekets.Where(x => x.SatisHareketTarih == dt).Sum(x => x.SatisHareketToplamTutar).ToString();
            ViewBag.d16 = deger16;


            return View();
        }

        public IActionResult BasitTablolar()
        {
            var sorgu1 = (from x in _context.Caris
                          where x.CariDurum == true
                          group x by x.CariSehir into g
                          select new SinifGrup
                          {
                              Sehir = g.Key,
                              Sayi = g.Count()
                          }).ToList();

            var sorgu2 = (from x in _context.Personels
                          group x by x.Departman.DepartmanAd into g
                          select new SinifGrup2
                          {
                              Departman = g.Key,
                              Adet = g.Count()
                          }).ToList();

            var sorgu3 = _context.Caris.Where(x => x.CariDurum == true).ToList();

            var sorgu4 = _context.Uruns.Where(x => x.UrunDurum == true).ToList();

            var sorgu5 = _context.Kategoris.ToList();

            var sorgu6 = (from x in _context.Uruns
                          group x by x.UrunMarka into g
                          select new SinifGrup3 
                          {
                              Marka = g.Key,
                              Sayi = g.Count(),
                          }).ToList();


            var tupleModel = new Tuple<
                IEnumerable<SinifGrup>, 
                IEnumerable<SinifGrup2>, 
                IEnumerable<Cari>, 
                IEnumerable<Urun>, 
                IEnumerable<Kategori>, 
                IEnumerable<SinifGrup3>
                >
                (sorgu1, sorgu2, sorgu3, sorgu4, sorgu5, sorgu6);

            return View(tupleModel);
        }

        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        public PartialViewResult Partial2()
        {
            return PartialView();
        }

        public PartialViewResult Partial3()
        {
            return PartialView();
        }
        public PartialViewResult Partial4()
        {
            return PartialView();
        }

        public PartialViewResult Partial5()
        {
            return PartialView();
        }

        public PartialViewResult Partial6()
        {
            return PartialView();
        }
    }
}
