using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Admin
	{
        [Key]
        public int AdminID { get; set; }
		[Column(TypeName = "Varchar"), StringLength(15)]
		public string KullaniciAd { get; set; }
		[Column(TypeName = "Varchar"), StringLength(25)]
		public string Sifre { get; set; }
		[Column(TypeName = "Varchar"), StringLength(1)]
		public string Yetki { get; set; }
    }
}
