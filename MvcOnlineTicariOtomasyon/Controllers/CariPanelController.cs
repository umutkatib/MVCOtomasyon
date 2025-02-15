using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
	[Authorize]
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
			var degerler = _context.Caris.FirstOrDefault(x => x.CariMail == mail);
			return View(degerler);
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
	}
}
