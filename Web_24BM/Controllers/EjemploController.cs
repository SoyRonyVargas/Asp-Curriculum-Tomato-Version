using Microsoft.AspNetCore.Mvc;
using Web_24BM.Models;

namespace Web_24BM.Controllers
{
    public class EjemploController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Tetx = "Esto es un texto desde el controlador";
            TempData["Texto2"] = "Esto es un texto temporal";

            Ejemplo model = new Ejemplo();
            model.Titulo = "Esto es un texto desde el controlador";


            return View(model);
        }
        [HttpPost]
        public IActionResult Index(Ejemplo ejemplo)
        {
            if(ModelState.IsValid) 
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index","Home");
            }
        }
    }
}
