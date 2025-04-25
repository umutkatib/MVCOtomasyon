using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
    public class Class3
    {
        public IEnumerable<SelectListItem> Kategoriler { get; set; }
        public IEnumerable<SelectListItem> Urunler { get; set; }
    }
}
