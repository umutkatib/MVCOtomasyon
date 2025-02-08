using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class SatisHareket
	{
        [Key]
        public int SatisHareketID { get; set; }
        public DateTime SatisHareketTarih { get; set; }
        [DisplayName("Adet")]
        public int SatisHareketAdet { get; set; }
        [DisplayName("Fiyat")]
        public decimal SatisHareketFiyat { get; set; }
        [DisplayName("Toplam Tutar")]
        public decimal SatisHareketToplamTutar { get; set; }

        public int UrunID { get; set; }
        public virtual Urun Urun { get; set; }
        public int CariID { get; set; }
        public virtual Cari Cari { get; set; }
        public int PersonelID { get; set; }
        public virtual Personel Personel { get; set; }
    }
}
