using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_24BM.Data;
using Web_24BM.Models;

namespace Web_24BM.Services
{
	public class ContactoService
	{

		private readonly ApplicationDbContext base_de_datos;
		
		public ContactoService(ApplicationDbContext base_de_datos)
		{
			this.base_de_datos = base_de_datos;
		}
        public string? GuardarArchivo(IFormFile archivo, string carpetaDestino)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return null; // No se proporcionó un archivo válido.
            }

            // Generar un nombre de archivo único con un uuid
            Guid uuid = Guid.NewGuid();
            
            string extension = Path.GetExtension(archivo.FileName);

            string nombreArchivo = $"{uuid}{extension}";

            // Combinar la ruta de destino con el nombre del archivo.
            string rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

            // Crear el directorio de destino si no existe.
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            return nombreArchivo;

        }

        public List<Contacto> getCurriculums()
        {
            return this.base_de_datos.Contactos.ToList();
        }
        public Contacto obtenerCurriculumPorId(int id)
        {
            return this.base_de_datos.Contactos
                    .Include(c => c.Habilidades)
                    .Include(c => c.Experiencia) // Añade más relaciones según sea necesario
                    .Include(c => c.Educacion) // Añade más relaciones según sea necesario
                    .Where(c => c.Id == id)
                    .FirstOrDefault()!;
        }

        //public Curriculum transformarContactoACurriculum(Contacto contacto)
        //{
        //    //Curriculum curriculum = new Curriculum(){ 
        //    //    Apellidos = contacto.Apellidos,
        //    //    Nombre= contacto.Nombre,
        //    //    Objetivo = contacto.Objetivo,
        //    //    Curp = contacto.Curp,
        //    //    Email = contacto.Email,
        //    //    Direccion = contacto.Direccion,
        //    //    Foto = (string)contacto.Foto
        //    //};

        //    return curriculum;
        //}

        public bool ActualizarContacto(Contacto model)
        {
            try
            {

                var elemento = this.base_de_datos.Contactos.Where(c => c.Id == model.Id).FirstOrDefault();

                if (elemento == null) return false;

                elemento.Nombre = model.Nombre;
                elemento.Apellidos = model.Apellidos;
                //elemento.Objetivo = model.Objetivo;
                //elemento.FechaNacimiento = model.FechaNacimiento;
                //elemento.Direccion = model.Direccion;
                //elemento.Curp = model.Curp;
                elemento.Email = model.Email;
                //elemento.Foto = archivo;

                this.base_de_datos.Contactos.Update(elemento);

                this.base_de_datos.SaveChanges();

                return true;

            }
            catch
            {
                return false;
            }
        }
        public bool eliminarCurriculum(int id)
        {
            try
            {

                // Eliminar registros de Experiencia relacionados con el ContactoId específico
                var experienciasAEliminar = this.base_de_datos.Experiencia.Where(e => e.ContactoId == id);
                
                this.base_de_datos.Experiencia.RemoveRange(experienciasAEliminar);

                this.base_de_datos.SaveChanges();

                var educacionAEliminar = this.base_de_datos.Educacion.Where(e => e.ContactoId == id);

                this.base_de_datos.Educacion.RemoveRange(educacionAEliminar);

                this.base_de_datos.SaveChanges();

                var habilidadesAEliminar = this.base_de_datos.Habilidades.Where(e => e.ContactoId == id);

                this.base_de_datos.Habilidades.RemoveRange(habilidadesAEliminar);

                this.base_de_datos.SaveChanges();

                var elemento = this.base_de_datos.Contactos.Where(c => c.Id == id).FirstOrDefault();
                
                this.base_de_datos.Contactos.Remove(elemento);

                this.base_de_datos.SaveChanges();

                return true;

            }
            catch(Exception e)
            {
                Debugger.Break();
                return false;
            }
        }

        public int CrearContacto(Curriculum curriculum)
		{

            string carpetaDestino = Path.Combine("wwwroot", "uploads");

            string archivo = this.GuardarArchivo( curriculum.Foto , carpetaDestino)!;

            Contacto nuevoContacto = new Contacto()
			{
				Nombre = curriculum.Nombre,
				Apellidos = curriculum.Apellidos,
				Email = curriculum.Email,
                Objetivos = curriculum.Objetivos,
                TituloLaboral = curriculum.TituloLaboral,
                Foto = archivo,
                Telefono = curriculum.Telefono,
                SitioWeb = curriculum.SitioWeb,
                Habilidades = curriculum.Habilidades,
                Experiencia = curriculum.Experiencia,
                Educacion = curriculum.Educacion
            };

			this.base_de_datos.Contactos.Add(nuevoContacto);

			this.base_de_datos.SaveChanges();

            return nuevoContacto.Id;

		}
	}
}
