using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
	public class CariPanelController : Controller
	{
		private Context _context;

		public CariPanelController(Context context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var mail = HttpContext.Session.GetString("CariMail");
			var cariID = _context.Caris.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
			var toplamSatis = _context.SatisHarekets.Where(x => x.CariID == cariID).Count();
			ViewBag.toplamSatis = toplamSatis;
			var toplamTutar = _context.SatisHarekets.Where(x => x.CariID == cariID).Sum(x => x.SatisHareketToplamTutar);
			ViewBag.toplamTutar = toplamTutar;
			var toplamUrun = _context.SatisHarekets.Where(x => x.CariID == cariID).Sum(y => y.SatisHareketAdet);
			ViewBag.toplamUrun = toplamUrun;
			var adSoyad = _context.Caris.Where(x => x.CariMail == mail).Select(x => x.CariAd + " " + x.CariSoyad).FirstOrDefault();
			ViewBag.adSoyad = adSoyad;
			ViewBag.mail = mail;
            var sehir = _context.Caris.Where(x => x.CariMail == mail).Select(x => x.CariSehir).FirstOrDefault();
			ViewBag.sehir = sehir;
			var degerler = _context.Mesajs.Where(x => x.MesajAlan == mail).ToList();
			var cari = _context.Caris.Where(x => x.CariMail == mail).FirstOrDefault();
            var duyurular = _context.Mesajs.Where(x => x.MesajGonderen == "admin").ToList();

            var tupleModel = new Tuple<
                IEnumerable<Mesaj>,
                Cari,
				IEnumerable<Mesaj>
                >
                (degerler, cari, duyurular);
            return View(tupleModel);
		}

		[HttpPost] 
		public IActionResult CariGuncelle(Cari c)
		{
			var mail = HttpContext.Session.GetString("CariMail");
			var cari = _context.Caris.FirstOrDefault(x => x.CariMail == mail);
			cari.CariAd = c.CariAd;
			cari.CariSoyad = c.CariSoyad;
			cari.CariSehir = c.CariSehir;
			cari.CariSifre = c.CariSifre;
			_context.SaveChanges();
            return RedirectToAction("Index");
		}

		public IActionResult Siparislerim()
		{
            var mail = HttpContext.Session.GetString("CariMail");
			var id = _context.Caris.Where(x => x.CariMail == mail.ToString()).Select(x => x.CariId).FirstOrDefault();
			var degerler = _context.SatisHarekets.Include(x => x.Urun).Where(x => x.CariID == id).ToList();
            return View(degerler);
		}

		public IActionResult CikisYap()
		{
			HttpContext.Session.Clear();
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Login");
		}

		public IActionResult GelenMesaj()
		{
            var mail = HttpContext.Session.GetString("CariMail");
            var mesajlar = _context.Mesajs.Where(x => x.MesajAlan == mail).Where(x => x.MesajDurum == true).OrderByDescending(x => x.MesajID).ToList();
			var gelenSayisi = _context.Mesajs.Count(x => x.MesajAlan == mail).ToString();
            var gidenSayisi = _context.Mesajs.Count(x => x.MesajGonderen == mail).ToString();
            ViewBag.gs = gelenSayisi;
            ViewBag.gsx = gidenSayisi;
            return View(mesajlar);
		}
        public IActionResult GidenMesaj()
        {
            var mail = HttpContext.Session.GetString("CariMail");
            var mesajlar = _context.Mesajs.Where(x => x.MesajGonderen == mail).Where(x => x.MesajDurum == true).OrderByDescending(x => x.MesajID).ToList();
            var gidenSayisi = _context.Mesajs.Count(x => x.MesajGonderen == mail).ToString();
            var gelenSayisi = _context.Mesajs.Count(x => x.MesajAlan == mail).ToString();
            ViewBag.gs = gelenSayisi;
            ViewBag.gsx = gidenSayisi;
            return View(mesajlar);
        }
		[HttpGet]
		public IActionResult MesajDetay(int id)
		{
            var mail = HttpContext.Session.GetString("CariMail");
			var mesaj = _context.Mesajs.Where(x => x.MesajID == id).ToList();
            var gidenSayisi = _context.Mesajs.Count(x => x.MesajGonderen == mail).ToString();
            var gelenSayisi = _context.Mesajs.Count(x => x.MesajAlan == mail).ToString();
            ViewBag.gs = gelenSayisi;
            ViewBag.gsx = gidenSayisi;
            return View(mesaj);
		}
		public IActionResult MesajSil(int id)
		{
			var mesaj = _context.Mesajs.Find(id);
			mesaj.MesajDurum = false;
			_context.SaveChanges();
			return RedirectToAction("GelenMesaj");
		}
        [HttpGet]
		public IActionResult YeniMesaj()
		{
            var mail = HttpContext.Session.GetString("CariMail");
            var gidenSayisi = _context.Mesajs.Count(x => x.MesajGonderen == mail).ToString();
            var gelenSayisi = _context.Mesajs.Count(x => x.MesajAlan == mail).ToString();
            ViewBag.gs = gelenSayisi;
            ViewBag.gsx = gidenSayisi;
            return View();
		}
		[HttpPost]
		public IActionResult YeniMesaj(Mesaj m)
		{
            var mail = HttpContext.Session.GetString("CariMail");
            m.MesajTarih = DateTime.Parse(DateTime.Now.ToShortDateString());
			m.MesajGonderen = mail;
			m.MesajDurum = true;
			_context.Mesajs.Add(m);
			_context.SaveChanges();
			return RedirectToAction();
		}

		public IActionResult KargoTakip(string p)
		{
			var kargo = _context.KargoDetays.Where(x => x.KargoDetayTakipKodu == p).ToList();
            if (!string.IsNullOrEmpty(p))
            {
                ViewBag.AramaYapildi = true; // Arama yapıldığını belirtiyoruz.
            }
            return View(kargo);
		}

		public IActionResult CariKargoTakipDetay(string id)
		{
			var degerler = _context.KargoTakips.Where(x => x.KargoTakipKodu == id).ToList();
			return View(degerler);
		}

		public PartialViewResult Partial1()
		{
			return PartialView();
		}

		public PartialViewResult Partial2()
		{
			return PartialView();
		}
	}
}
