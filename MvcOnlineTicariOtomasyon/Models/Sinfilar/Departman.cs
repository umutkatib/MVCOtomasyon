using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class Departman
	{
        [Key]
        public int DepartmanID { get; set; }
		[Column(TypeName = "Varchar"), StringLength(35), DisplayName("Departman Adı")]
		public string DepartmanAd { get; set; }
        public bool DepartmanDurum { get; set; }

        public ICollection<Personel> Personels { get; set; }
    }
}
