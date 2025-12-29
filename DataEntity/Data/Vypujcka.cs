using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataEntity.Base;
using DataEntity.Data; // Pro jistotu

namespace DataEntity.Data
{
    public class Vypujcka : BaseEntity
    {

        public int ZakaznikId { get; set; }

        [ForeignKey("ZakaznikId")]
        public virtual Zakaznik Zakaznik { get; set; } = null!;

        public int NaradiId { get; set; }

        [ForeignKey("NaradiId")]
        public virtual Naradi Naradi { get; set; } = null!;

        public DateTime DatumVypujcky { get; set; } = DateTime.Now;
        public DateTime DatumVraceniPlan { get; set; }
        public DateTime? DatumVraceniSkutecne { get; set; }

        public decimal Cena { get; set; }
        public decimal Penale { get; set; }
    }
}