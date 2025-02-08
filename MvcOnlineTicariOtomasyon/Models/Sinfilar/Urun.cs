using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Urun
	{
        [Key]
        public int UrunID { get; set; }
        [Column(TypeName = "Varchar"), StringLength(35), DisplayName("Ad")]
        public string UrunAd { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35), DisplayName("Marka")]
        public string UrunMarka { get; set; }
        [DisplayName("Stok")]
        public short UrunStok { get; set; }
        [DisplayName("Alış Fiyatı")]
        public decimal UrunAlisFiyat { get; set; }
        [DisplayName("Satış Fiyatı")]
        public decimal UrunSatisFiyat { get; set; }
        [DisplayName("Durum")]
        public bool UrunDurum { get; set; }
		[Column(TypeName = "Varchar"), StringLength(300),DisplayName("Görsel")]
		public string UrunGorsel { get; set; }

        public int KategoriID { get; set; }
        public virtual Kategori Kategori { get; set; }
		public ICollection<SatisHareket> SatisHarekets { get; set; }

	}
}
