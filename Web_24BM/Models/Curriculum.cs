using System.ComponentModel.DataAnnotations;


namespace Web_24BM.Models
{
    public class Curriculum
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(50, ErrorMessage ="El campo Nombre no debe seperar los 50 caracteres")]
        public string Nombre { get; set; }
        
        [StringLength(50, ErrorMessage = "El campo Apellidos no debe seperar los 50 caracteres")]
        public string Apellidos { get; set; }
        
        public string Email { get; set; }

        
        [Required(ErrorMessage = "El fecha de nacimiento es requerida.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Sitio Web es requerido.")]
        //[RegularExpression(@"^(https?://)?([\w\.-]+)\.([a-z\.]{2,6})([/\w\.-]*)*/?$",
        //     ErrorMessage = "El formato del Sitio Web no es válido.")]

        public string SitioWeb { get; set; }

        public List<Habilidad>? Habilidades { get; set; }
        public List<Experiencia>? Experiencia { get; set; }

        public IFormFile? Foto { get; set; }
        public List <DatosLaboral>? DatosLaborales { get; set;}

    }
}
