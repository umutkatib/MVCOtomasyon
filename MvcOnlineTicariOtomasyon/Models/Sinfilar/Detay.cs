using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
    public class Detay
    {
        [Key]
        public int DetayID { get; set; }
        [Column(TypeName ="Varchar"), StringLength(35)]
        public string UrunAd { get; set; }
        [Column(TypeName = "Varchar"), StringLength(1250)]
        public string UrunBilgi { get; set; }
    }
}
