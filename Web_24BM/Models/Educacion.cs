using System.ComponentModel.DataAnnotations;

namespace Web_24BM.Models
{
    public class Educacion
    {
        [Key]
        public int Id { get; set; }

        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }

        public int? ContactoId { get; set; }
        public Contacto? Contacto { get; set; }
    }
}
