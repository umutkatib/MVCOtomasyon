using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Fatura
	{
        [Key]
        public int FaturaID { get; set; }
		[Column(TypeName = "Char"), StringLength(1), DisplayName("Seri No")]
		public string FaturaSeriNo { get; set; }
		[Column(TypeName = "Varchar"), StringLength(6), DisplayName("Sıra No")]
		public string FaturaSiraNo { get; set; }
		[DisplayName("Tarih")]
        public DateTime FaturaTarih { get; set; }
        [Column(TypeName = "char"), StringLength(5), DisplayName("Saat")]
        public string FaturaSaat { get; set; }
		[Column(TypeName = "Varchar"), StringLength(50), DisplayName("Vergi Dairesi")]
		public string FaturaVergiDairesi { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35), DisplayName("Teslim Eden")]
		public string FaturaTeslimEden { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35), DisplayName("Teslim Alan")]
		public string FaturaTeslimAlan { get; set; }

        public decimal FaturaToplamTutar { get; set; }

        public ICollection<FaturaKalem> FaturaKalems { get; set; }
    }
}
