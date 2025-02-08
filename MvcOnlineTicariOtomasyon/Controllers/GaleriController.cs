using Microsoft.AspNetCore.Mvc;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {
        private Context _context;

        public GaleriController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var degerler = _context.Uruns.Where(x => x.UrunDurum == true).ToList();
            return View(degerler);
        }
    }
}
