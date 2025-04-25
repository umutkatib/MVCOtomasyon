using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcOnlineTicariOtomasyon.Models.Sinfilar;
using MvcOnlineTicariOtomasyon.Models.Sinfilar.Context;
using System;
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
            var fkalemler = _context.FaturaKalems.Where(x => x.FaturaID == id).Where(x => x.FaturaKalemDurum == true).ToList();
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

        //public IActionResult FaturaKalemSil(int id)
        //{
        //    var fkalem = _context.FaturaKalems.Find(id);
        //    fkalem.FaturaKalemDurum = false;
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public IActionResult Dinamik()
        {
            DinamikF dinamikF = new DinamikF();
            dinamikF.Faturalar = _context.Faturas.ToList();
            dinamikF.FaturaKalem = _context.FaturaKalems.ToList();
            return View(dinamikF);
        }

        public IActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo, 
            DateTime FaturaTarih, string FaturaVergiDairesi, string FaturaSaat,
            string FaturaTeslimEden, string FaturaTeslimAlan, string FaturaToplamTutar, FaturaKalem[] FKalemler)
        {
            Fatura f = new Fatura();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo = FaturaSiraNo;
            f.FaturaTarih = FaturaTarih;
            f.FaturaVergiDairesi = FaturaVergiDairesi;
            f.FaturaSaat = FaturaSaat;
            f.FaturaTeslimEden = FaturaTeslimEden;
            f.FaturaTeslimAlan = FaturaTeslimAlan;
            f.FaturaToplamTutar = decimal.Parse(FaturaToplamTutar);
            _context.Faturas.Add(f);
            _context.SaveChanges();

            foreach (var kalem in FKalemler)
            {
                kalem.FaturaID = f.FaturaID;
                kalem.FaturaKalemDurum = true;
                _context.FaturaKalems.Add(kalem);
            }

            _context.SaveChanges();


            return Json("İşlem Başarılı");
        }
    }
}
