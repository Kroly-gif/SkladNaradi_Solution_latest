using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Přidej tento using

namespace DataEntity.Base
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // TOTO ŘÍKÁ: "Databáze, vygeneruj číslo sama hned při vložení"
        public int Id { get; set; }
    }
}