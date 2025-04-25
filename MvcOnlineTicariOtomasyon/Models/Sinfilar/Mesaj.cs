using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcOnlineTicariOtomasyon.Models.Sinfilar
{
    public class Mesaj
    {
        [Key]
        public int MesajID { get; set; }
        [Column(TypeName ="Varchar(50)")]
        public string MesajGonderen { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string MesajAlan { get; set; }
        [Column(TypeName = "Varchar(75)")]
        public string MesajKonu { get; set; }
        [Column(TypeName = "Varchar(1000)")]
        public string MesajIcerik { get; set; }
        public bool MesajDurum { get; set; }
        [Column(TypeName = "Date")]
        public DateTime MesajTarih { get; set; }
    }
}
