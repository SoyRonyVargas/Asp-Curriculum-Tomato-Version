using System.Net.Sockets;

namespace Web_24BM.Models
{
    public class EmailViewModel
    {
        public string Email { get; set; }
        public string Mensaje { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public TimeOnly HoraEntrada { get; set; }
        public SelectMode Turno { get; set; }

    }
}
