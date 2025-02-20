using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
    public class KargoTakip
    {
        [Key]
        public int KargoTakipID { get; set; }
        [Column(TypeName = "VarChar(10)")]
        public string KargoTakipKodu { get; set; }
        [Column(TypeName = "VarChar(100)")]
        public string KargoTakipAciklama { get; set; }
        public DateTime KargoTakipTarih { get; set; }
    }
}
