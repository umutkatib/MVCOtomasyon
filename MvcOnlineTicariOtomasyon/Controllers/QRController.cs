using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class QRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Qr/Index/{kod}")]
        public IActionResult Index(string kod)
        {
            if (string.IsNullOrEmpty(kod))
            {
                ViewBag.ErrorMessage = "Geçerli bir kod bulunamadı.";
                return View();
            }
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qRCode = qRCodeGenerator.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap img = qRCode.GetGraphic(10))
                {
                    img.Save(ms, ImageFormat.Png);
                    ViewBag.qrCodeImg = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                };
            }
            ViewBag.kod = kod;
            return View();
        }
    }
}
