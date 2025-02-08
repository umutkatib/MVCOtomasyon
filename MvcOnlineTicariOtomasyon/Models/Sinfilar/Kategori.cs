using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Kategori
	{
        [Key]
        public int KategoriID { get; set; }
		[Column(TypeName = "Varchar"), StringLength(50), DisplayName("Kategori Adı")]
		public string KategoriAd { get; set; }

        public ICollection<Urun> Uruns { get; set; }
    }
}
