using DataEntity.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataEntity.Data
{
    public class Zakaznik : BaseEntity
    {

        [Required]
        public string Jmeno { get; set; } = string.Empty;

        [Required]
        public string Prijmeni { get; set; } = string.Empty;

        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Adresa { get; set; } = string.Empty;
        public string Organizace { get; set; } = string.Empty;
        public string Poznamka { get; set; } = string.Empty;

        public bool Ban { get; set; } = false;

        // Tady byla chyba - teď už Vypujcka uvidí, protože sdílí namespace
        public virtual ICollection<Vypujcka> Vypujcky { get; set; } = new List<Vypujcka>();
    }
}