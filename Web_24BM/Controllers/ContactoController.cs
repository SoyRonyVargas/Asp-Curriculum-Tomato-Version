using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Web_24BM.Models;
using Web_24BM.Services;

namespace Web_24BM.Controllers
{
    public class ContactoController : Controller
    {
        private readonly IEmailSenderService _emailSender;

        public ContactoController(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Formulario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EnviarEmail(string email, string comentario)
        {
            TempData["Email"] = email;
            TempData["Comentario"] = comentario;
            EnviarEmailSmtp(email);
            return View("Index", "Contacto");
        }

        [HttpPost]
        public IActionResult EnviarFormulario(EmailViewModel model)
        {
            TempData["Email"] = model.Email;
            TempData["Comentario"] = model.Mensaje;
            var result = _emailSender.SendEmail(model.Email);
            if (!result)
            {
                TempData["Email"] = null;
                TempData["EmailError"] = "Ocurrio un error";
            }
            return View("Formulario", model);

        }

        public bool EnviarEmailSmtp(string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.shapp.mx", 587);

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("moises.puc@shapp.mx", "Dhaserck_999");
            mail.From = new MailAddress("moises.puc@shapp.mx", "Administrador");
            mail.To.Add(email);
            mail.Subject = "Aviso de contacto";
            mail.IsBodyHtml = true;
            mail.Body = $"Se ha contactado la persona con el correo {email} para solicitar informacion";
            smtp.Send(mail);
            return true;
        }
    }
}