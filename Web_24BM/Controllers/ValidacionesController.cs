using Microsoft.AspNetCore.Mvc;
using Web_24BM.Models;
using Web_24BM.Services;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Web_24BM.Controllers
{
    public class ValidacionesController : Controller
    {

        private readonly ContactoService contactoService;
        public ValidacionesController(ContactoService contactoService)
        {
            this.contactoService = contactoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Curriculum()
        {
            return View();
        }

        public IActionResult ListadoCurriculum()
        {
            var datos = this.contactoService.getCurriculums();
            return View("ListadoCurriculum" , datos);
        }

        public IActionResult DetallePorId(int id)
        {
            
            Contacto model = this.contactoService.obtenerCurriculumPorId(id);

            if (model == null) return RedirectToActionPermanent("Index");

            return View("DetallePorId", model);
        }

        public IActionResult CurriculumPorId(int id)
        {

            Contacto model = this.contactoService.obtenerCurriculumPorId(id);

            if (model == null) return RedirectToActionPermanent("Index");

            //Curriculum datos = this.contactoService.transformarContactoACurriculum(model);

            return View("CurriculumPorId", model);

        }

        [HttpPost]
        public IActionResult EliminarCurriculum(int id)
        {
            var Eliminado = this.contactoService.eliminarCurriculum(id);
            var datos = this.contactoService.getCurriculums();
            TempData["Eliminado"] = Eliminado;
            return RedirectToAction("ListadoCurriculum");
        }

        [HttpPost]
        public IActionResult ActualizarCurriculum(int id)
        {
            
            Contacto model = this.contactoService.obtenerCurriculumPorId(id);

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true // Otras opciones que puedas necesitar
            };

            var serializedModel = JsonSerializer.Serialize(model, jsonOptions);

            ViewData["SerializedModel"] = serializedModel;

            return View("ActualizarCurriculum", model);
        }



        [HttpPost]
        public IActionResult ActualizarContactoFinal(Contacto model)
        {

            if (!ModelState.IsValid) return Content("false");

            this.contactoService.ActualizarContacto(model);

            TempData["Completado"] = "Datos guardados correctamente";

            TempData["Actualizado"] = true;

            return Content("true");

        }

        public bool EsDireccionDeCorreoValida(string correo)
        {
            // Expresión regular para validar una dirección de correo electrónico
            string patronCorreo = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Utiliza la expresión regular para validar el correo
            bool esValido = Regex.IsMatch(correo, patronCorreo);

            return esValido;
        }
        public bool ValidarCurp(string curp)
        {
            // Expresión regular para validar una CURP
            string patron = @"^[A-Z]{4}[0-9]{6}[H,M][A-Z]{5}[A-Z0-9][0-9]$";

            // Comprobar si la CURP coincide con el patrón
            if (Regex.IsMatch(curp, patron))
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public IActionResult EnviarFormulario([FromForm] Curriculum model)
        {

            if (!ModelState.IsValid) return Content("false");

            bool resultCorreo = this.EsDireccionDeCorreoValida(model.Email);

            if (!resultCorreo)
            {

                TempData["ErrorCorreo"] = "Ingresa un correo valido";

                return View("Index", model);

            }
            
            int id = this.contactoService.CrearContacto(model);
            
            TempData["Completado"] = "Datos guardados correctamente";

            return Content(id.ToString());

        }

    }
}

