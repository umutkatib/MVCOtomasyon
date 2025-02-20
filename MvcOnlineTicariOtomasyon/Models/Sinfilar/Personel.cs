using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Personel
	{
		[Key]
        public int PersonelID { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35), Required, DisplayName("Ad")]
		public string PersonelAd { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35), Required, DisplayName("Soyad")]
		public string PersonelSoyad { get; set; }
		[Column(TypeName = "Varchar"), StringLength(300), Required, DisplayName("Görsel")]
		public string PersonelGorsel { get; set; }
        [Column(TypeName = "Varchar"), StringLength(100), DisplayName("Ülke")]
        public string PersonelUlke { get; set; }
        [Column(TypeName = "Varchar"), StringLength(200), DisplayName("Hakkında")]
        public string PersonelHakkinda { get; set; }
        [Column(TypeName = "Varchar"), StringLength(50), DisplayName("Telefon")]
        public string PersonelTelefon { get; set; }
        [Column(TypeName = "Varchar"), StringLength(70), DisplayName("Adres")]
        public string PersonelAdres { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }


        public int DepartmanID { get; set; }
        public virtual Departman Departman { get; set; }

    }
}
