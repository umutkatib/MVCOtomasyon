using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        private Context _context;

        public KargoController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var kargolar = _context.KargoDetays.ToList();
            return View(kargolar);
        }

        [HttpGet]
        public IActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "K", "L", "M" };
            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length);
            k2 = rnd.Next(0, karakterler.Length);
            k3 = rnd.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2.ToString() + karakterler[k2] + s3.ToString() + karakterler[k3];
            ViewBag.TakipKodu = kod;
            return View();
        }
        [HttpPost]
        public IActionResult YeniKargo(KargoDetay kd)
        {
            _context.KargoDetays.Add(kd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult KargoTakip(string id)
        {
            var degerler = _context.KargoTakips.Where(x => x.KargoTakipKodu == id).ToList();
            ViewBag.kod = id;
            return View(degerler);
        }

        [HttpGet]
        public IActionResult KargoDetayEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KargoDetayEkle(KargoTakip kt, string id)
        {
            kt.KargoTakipKodu = id;
            _context.KargoTakips.Add(kt);
            _context.SaveChanges();
            return RedirectToAction("KargoTakip", new { id = kt.KargoTakipKodu });
        }
    }
}
