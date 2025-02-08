using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
	public class KategoriController : Controller
	{
		private Context _context;

        public KategoriController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
		{
			var degerler = _context.Kategoris.ToList();
			return View(degerler);
		}

		[HttpGet]
		public IActionResult KategoriEkle()
		{
			return View();
		}
        [HttpPost]
        public IActionResult KategoriEkle(Kategori k)
        {
            _context.Add(k);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult KategoriSil(int id)
        {
            var deger = _context.Kategoris.Find(id);
            _context.Remove(deger);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult KategoriGetir(int id)
        {
            var kategori = _context.Kategoris.Find(id);
            return View("KategoriGetir", kategori);
        }
        [HttpPost]
        public IActionResult KategoriGuncelle(Kategori k)
        {
            var kategori = _context.Kategoris.Find(k.KategoriID);
            kategori.KategoriAd = k.KategoriAd;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
