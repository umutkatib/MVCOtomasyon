using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayID { get; set; }
        [Column(TypeName = "VarChar(200)"), DisplayName("Açıklama")]
        public string KargoDetayAciklama { get; set; }
        [Column(TypeName = "VarChar(14)"), DisplayName("Takip Kodu")]
        public string KargoDetayTakipKodu { get; set; }
        [Column(TypeName = "VarChar(30)"), DisplayName("Personel")]
        public string KargoDetayPersonel { get; set; }
        [Column(TypeName = "VarChar(30)"), DisplayName("Alıcı")]
        public string KargoDetayAlici { get; set; }
        [DisplayName("Tarih")]
        public DateTime KargoDetayTarih { get; set; }
    }
}
