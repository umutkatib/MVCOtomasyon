using System.Collections.Generic;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
    public class DinamikF
    {
        public IEnumerable<Fatura> Faturalar { get; set; }
        public IEnumerable<FaturaKalem> FaturaKalem { get; set; }
    }
}
