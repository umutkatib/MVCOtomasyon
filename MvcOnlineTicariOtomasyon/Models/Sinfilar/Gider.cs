using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Gider
	{
        [Key]
        public int GiderID { get; set; }
		[Column(TypeName = "Varchar"), StringLength(200)]
		public string GiderAciklama { get; set; }
        public DateTime GiderTarih { get; set; }
        public decimal GiderTutar { get; set; }
    }
}
