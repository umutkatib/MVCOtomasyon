using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System.Linq;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        private Context _context;

        public FaturaController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var faturalar = _context.Faturas.Include(f => f.FaturaKalems).ToList();

            foreach (var fatura in faturalar)
            {
                fatura.FaturaToplamTutar = fatura.FaturaKalems.Sum(k => k.FaturaKalemTutar);
            }
            return View(faturalar);
        }

        [HttpGet]
        public IActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FaturaEkle(Fatura f)
        {
            _context.Faturas.Add(f);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult FaturaGetir(int id)
        {
            var fatura = _context.Faturas.Find(id);
            return View(fatura);
        }

        public IActionResult FaturaGuncelle(Fatura f)
        {
            var fatura = _context.Faturas.Find(f.FaturaID);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSiraNo = f.FaturaSiraNo;
            fatura.FaturaVergiDairesi = f.FaturaVergiDairesi;
            fatura.FaturaSaat = f.FaturaSaat;
            fatura.FaturaTarih = f.FaturaTarih;
            fatura.FaturaTeslimEden = f.FaturaTeslimEden;
            fatura.FaturaTeslimAlan = f.FaturaTeslimAlan;
            _context.SaveChanges();
            return RedirectToAction("Index");
        } 

        public IActionResult FaturaDetay(int id)
        {
            var fkalemler = _context.FaturaKalems.Where(x => x.FaturaID == id).ToList();
            ViewBag.FaturaId = id;
            return View(fkalemler);
        }

        [HttpGet]
        public IActionResult YeniKalem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YeniKalem(FaturaKalem fk, int id)
        {
            fk.FaturaID = id;
            _context.FaturaKalems.Add(fk);
            _context.SaveChanges();
            return RedirectToAction("FaturaDetay", new { id = fk.FaturaID});
        }

        public IActionResult FaturaKalemGetir(int id)
        {
            var fkalem = _context.FaturaKalems.Find(id);
            return View(fkalem);
        }

        public IActionResult FaturaKalemGuncelle(FaturaKalem fk)
        {
            var fkalem = _context.FaturaKalems.Find(fk.FaturaKalemID);
            fkalem.FaturaKalemAciklama = fk.FaturaKalemAciklama;
            fkalem.FaturaKalemMiktar = fk.FaturaKalemMiktar;
            fkalem.FaturaKalemTutar = fk.FaturaKalemTutar;
            fkalem.FaturaKalemBirimFiyat = fk.FaturaKalemBirimFiyat;
            _context.SaveChanges();
            return RedirectToAction("");
        }
    }
}
