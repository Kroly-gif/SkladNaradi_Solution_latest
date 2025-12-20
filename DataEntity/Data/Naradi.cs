using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataEntity.Data
{
    public class Naradi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nazev { get; set; } = string.Empty;

        public string Vykon { get; set; } = string.Empty;
        public string Umisteni { get; set; } = string.Empty;
        public string Hmotnost { get; set; } = string.Empty;
        public string Popis { get; set; } = string.Empty;
        public string Poznamka { get; set; } = string.Empty;

        // NOVÁ VLASTNOST - Cena za 1 den půjčení
        public decimal CenaZaDen { get; set; } = 0;

        public bool Dostupnost { get; set; } = true;

        public virtual ICollection<Vypujcka> Vypujcky { get; set; } = new List<Vypujcka>();
    }
}