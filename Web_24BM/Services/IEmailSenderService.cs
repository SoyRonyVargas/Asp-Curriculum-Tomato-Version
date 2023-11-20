using Web_24BM.Models;

namespace Web_24BM.Services
{
    public interface IEmailSenderService
    {
        bool SendEmail(string email);
        
        bool ProcesarS(EmailViewModel email);

        bool SendEmailwithData(MensajeViewModel model);
    }
}
