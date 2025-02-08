using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        private Context _context;

        public CariController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cariler = _context.Caris.Where(x => x.CariDurum == true).ToList();
            return View(cariler);
        }

        [HttpGet]
        public IActionResult CariEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CariEkle(Cari c)
        {
            c.CariDurum = true;
            _context.Caris.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CariSil(int id)
        {
            var cari = _context.Caris.Find(id);
            cari.CariDurum = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CariGetir(int id)
        {
            var cari = _context.Caris.Find(id);
            return View(cari);
        } 
        public IActionResult CariGuncelle(Cari c)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cari = _context.Caris.Find(c.CariId);
            cari.CariAd = c.CariAd;
            cari.CariSoyad = c.CariSoyad;
            cari.CariSehir = c.CariSehir;
            cari.CariMail = c.CariMail;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CariSatisGecmis(int id)
        {
            var satislar = _context.SatisHarekets.Include(x => x.Urun).Include(y => y.Personel).Where(x => x.CariID == id).ToList();
            var cari = _context.Caris.Where(x => x.CariId == id).Select(x => x.CariAd + " " + x.CariSoyad).FirstOrDefault();
            ViewBag.ca = cari;
            return View(satislar);
        }

        public IActionResult PasifCariler()
        {
            var pasifCariler = _context.Caris.Where(x => x.CariDurum == false).ToList();
            return View(pasifCariler);
        }

        public IActionResult CariAktifYap(int id)
        {
            var cari = _context.Caris.Find(id);
            cari.CariDurum = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
