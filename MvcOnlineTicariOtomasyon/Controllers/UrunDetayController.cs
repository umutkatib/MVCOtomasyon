using Microsoft.AspNetCore.Mvc;
using MvcOnlineTicariOtomasyon.Models;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        private Context _context;

        public UrunDetayController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Class1 cs = new Class1();
            //var urun = _context.Uruns.Where(x => x.UrunDurum == true).Where(x => x.UrunID == 1).ToList();   
            cs.UrunDeger = _context.Uruns.Where(x => x.UrunDurum == true).Where(x => x.UrunID == 1).ToList();
            cs.UrunDetay = _context.Detays.Where(x => x.DetayID == 1).ToList();
            return View(cs);
        }
    }
}
