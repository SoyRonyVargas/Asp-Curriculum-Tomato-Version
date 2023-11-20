using System.ComponentModel.DataAnnotations;

namespace Web_24BM.Models
{
    public class RecentWork
    {
        [Key]
        public int? Id { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }

    }
}
