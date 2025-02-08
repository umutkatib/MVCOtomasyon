using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
	public class FaturaKalem
	{
		[Key]
		public int FaturaKalemID { get; set; }
		[Column(TypeName = "Varchar"), StringLength(200), DisplayName("Açıklama")]
		public string FaturaKalemAciklama { get; set; }
        [DisplayName("Miktar")]
        public int FaturaKalemMiktar { get; set; }
        [DisplayName("Birim Fiyat")]
        public decimal FaturaKalemBirimFiyat { get; set; }
        [DisplayName("Tutar")]
        public decimal FaturaKalemTutar { get; set; }
        public bool FaturaKalemDurum { get; set; }

        public int FaturaID { get; set; }
        public virtual Fatura Fatura { get; set; }
    }
}
