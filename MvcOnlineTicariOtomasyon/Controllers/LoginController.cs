using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        private Context _context;

		public LoginController(Context context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
		[HttpPost]
		public PartialViewResult Partial1(Cari c)
		{
			c.CariDurum = true;
			_context.Caris.Add(c);
			_context.SaveChanges();
			return PartialView("Index");
		}

		public PartialViewResult Partial2()
		{
			return PartialView();
		}

		[HttpPost]
		public IActionResult CariLogin1(string CariMail, string CariSifre)
		{
			var bilgiler = _context.Caris.Where(x => x.CariDurum == true).FirstOrDefault(
				x => x.CariMail == CariMail && x.CariSifre == CariSifre);

			if (bilgiler != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, bilgiler.CariMail),
					new Claim("CariId", bilgiler.CariId.ToString())
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				HttpContext.Session.SetString("CariMail", bilgiler.CariMail);


				return RedirectToAction("Index", "CariPanel");
			}

			ViewBag.Error = "Geçersiz kullanıcı adı veya şifre.";
			return View("Index");
		}

        public PartialViewResult Partial3()
        {
            return PartialView();
        }
		public IActionResult AdminLogin(string KullaniciAd, string Sifre)
		{
			var bilgiler = _context.Admins.FirstOrDefault(
				x => x.KullaniciAd == KullaniciAd && x.Sifre == Sifre);

			if (bilgiler != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, bilgiler.KullaniciAd),
					new Claim("AdminID", bilgiler.AdminID.ToString())
				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				HttpContext.Session.SetString("KullaniciAd", bilgiler.KullaniciAd);


				return RedirectToAction("Index", "Kategori");
			}

			ViewBag.Error = "Geçersiz kullanıcı adı veya şifre.";
			return View("Index");
		}
    }
}
