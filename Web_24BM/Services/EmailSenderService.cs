using System.Net.Mail;
using System.Net;
using Web_24BM.Models;
using System.Net.Http;

namespace Web_24BM.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public bool SendEmail(string email)
        {
            bool result = false;
            try
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

                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public bool ProcesarS(EmailViewModel email)
        {
            bool result = false;

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("mail.shapp.mx", 587);

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("moises.puc@shapp.mx", "Dhaserck_999");

                mail.From = new MailAddress("moises.puc@shapp.mx", "Administrador");
                mail.To.Add(email.Email);
                mail.Subject = "Información de contacto: ";
                mail.IsBodyHtml = true;
                mail.Body = $"Se ha contactado el cliente con los siguientes datos:" +
                    $"Nombre: {email.Nombre}" +
                    $"Apellido: {email.Apellido}" +
                    $"Correo: {email.Email}" +
                    $"Fecha de Nacimiento: {email.FechaNacimiento}" +
                    $"Hora de Entrada: {email.HoraEntrada}" +
                    $"Turno: {email.Turno}" +
                    $"Mensaje: {email.Mensaje}";

                smtpClient.Send(mail);

                result = true;

            }
            catch (Exception ex)
            {

            }


            return result;
        }

        public bool SendEmailwithData(MensajeViewModel model)
        {
            bool result = false;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("mail.shapp.mx", 587);

                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("moises.puc@shapp.mx", "Dhaserck_999");
                mail.From = new MailAddress("moises.puc@shapp.mx", "Administrador");
                mail.To.Add(model.Email);
                mail.Subject = model.SubJect;
                mail.IsBodyHtml = true;
                mail.Body = model.Content;
                smtp.Send(mail);

                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
