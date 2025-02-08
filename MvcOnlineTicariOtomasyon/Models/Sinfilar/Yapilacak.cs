using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
    public class Yapilacak
    {
        [Key]
        public int YapilacakID { get; set; }
        [Column(TypeName = "Varchar"), StringLength(35), DisplayName("Başlık")]
        public string YapilacakBaslik { get; set; }
        [Column(TypeName = "bit"), DisplayName("Durum")]
        public bool YapilacakDurum { get; set; }
    }
}
