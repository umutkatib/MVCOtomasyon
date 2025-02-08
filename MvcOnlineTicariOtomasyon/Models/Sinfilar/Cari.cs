using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Cari
	{
        [Key]
        public int CariId { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35, ErrorMessage ="En fazla 35 karakter"), Required(ErrorMessage ="Ad boş geçilemez"), DisplayName("Ad")]
		public string CariAd { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35, ErrorMessage = "En fazla 35 karakter"), Required(ErrorMessage = "Soyad boş geçilemez"), DisplayName("Soyad")]
		public string CariSoyad { get; set; }
		[Column(TypeName = "Varchar"), StringLength(15), DisplayName("Şehir")]
		public string CariSehir { get; set; }
		[Column(TypeName = "Varchar"), StringLength(50), DisplayName("Mail")]
		public string CariMail { get; set; }
        public bool CariDurum { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }

	}
}
