using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;
using System;
using System.Reflection;
using Pechka.DLL.Abstract;

namespace Pechka.DLL.Cncrete
{
    public class EmailService:IEmailService
    {
        public bool ConfirmEmailSend(User model,string body)
        {
            try
            {
                MailAddress from = new MailAddress("address", "Registration Pechka_Epam");
                MailAddress to = new MailAddress(model.Email);
                MailMessage simpleMessage = new MailMessage(from, to);
                simpleMessage.Subject = "Подтверждение регистрации";
                simpleMessage.Body = body;
                simpleMessage.IsBodyHtml = true;
                SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.yandex.ru", 25);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential("address", "pass");
                smtp.Send(simpleMessage);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
