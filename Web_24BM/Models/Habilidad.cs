using System.ComponentModel.DataAnnotations;

namespace Web_24BM.Models
{
    public class Habilidad
    {
        [Key]
        public int? Id { get; set; }
        public string? Titulo { get; set; }

        // Nueva propiedad para la relación
        public int? ContactoId { get; set; }
        public Contacto? Contacto { get; set; }
    }
}
