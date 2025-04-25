using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        private Context _context;

        public DepartmanController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departmanlar = _context.Departmans.Where(x => x.DepartmanDurum == true).ToList();
            return View(departmanlar);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DepartmanEkle()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult DepartmanEkle(Departman d)
        {
            d.DepartmanDurum = true;
            _context.Departmans.Add(d);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DepartmanSil(int id)
        {
            var departman = _context.Departmans.Find(id);
            departman.DepartmanDurum = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DepartmanGetir(int id)
        {
            var departman = _context.Departmans.Find(id);
            return View(departman);
        }
        [HttpPost]
        public IActionResult DepartmanGuncelle(Departman d)
        {
            var departman = _context.Departmans.Find(d.DepartmanID);
            departman.DepartmanAd = d.DepartmanAd;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DepartmanDetay(int id)
        {
            var degerler = _context.Personels.Where(x => x.DepartmanID == id).ToList();
            var dptman = _context.Departmans.Where(x => x.DepartmanID == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.dpm = dptman;

            return View(degerler);
        }

        public IActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = _context.SatisHarekets.Include(x => x.Urun).Include(y => y.Cari).Where(x => x.PersonelID == id).ToList();
            var dp = _context.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dp = dp;
            return View(degerler);
        }
    }
}
