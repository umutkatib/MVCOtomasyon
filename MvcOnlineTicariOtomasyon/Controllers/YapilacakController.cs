using Microsoft.AspNetCore.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        private Context _context;

        public YapilacakController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var deger1 = _context.Caris.Where(x => x.CariDurum == true).Count().ToString();
            ViewBag.Deger1 = deger1;
            var deger2 = _context.Uruns.Where(x => x.UrunDurum == true).Count().ToString();
            ViewBag.Deger2 = deger2;
            var deger3 = _context.Kategoris.Count().ToString();
            ViewBag.Deger3 = deger3;
            var deger4 = (from x in _context.Caris select x.CariSehir).Distinct().Count().ToString();
            ViewBag.Deger4 = deger4;

            var yapilacaklar = _context.Yapilacaks.Where(x => x.YapilacakDurum  == true).ToList();
            return View(yapilacaklar);
        }

        [HttpPost]
        public IActionResult YapilacakEkle(Yapilacak y)
        {
            if (string.IsNullOrWhiteSpace(y.YapilacakBaslik))
            {
                return RedirectToAction("Index"); 
            }

            y.YapilacakDurum = true;
            _context.Yapilacaks.Add(y);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
