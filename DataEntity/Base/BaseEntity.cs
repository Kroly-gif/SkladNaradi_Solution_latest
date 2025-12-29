using System.ComponentModel.DataAnnotations;

namespace DataEntity.Base
{
    // 1. Musí tu být "public"
    public class BaseEntity
    {
        [Key]
        // 2. Musí tu být "public" (pokud tam public chybí, je to ten problém!)
        public int Id { get; set; }
    }
}