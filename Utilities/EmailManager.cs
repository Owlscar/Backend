using System.Net.Mail;
using System.Net;

namespace Backend.Utilities
{
    public class EmailManager
    {
        private SmtpClient cliente;
        private MailMessage? email; // Marked as nullable to resolve CS8618
        private string Host = "smtp.gmail.com";
        private int Port = 587;
        private string User = "owlblack10@gmail.com";
        private string Password = "ydhuxsfalvmwhzgc"; // Contraseña de Aplicación
        private bool EnabledSSL = true;

        public EmailManager()
        {
            cliente = new SmtpClient(Host, Port)
            {
                EnableSsl = EnabledSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(User, Password)
            };
        }

        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtml = false)
        {
            email = new MailMessage(User, destinatario, asunto, mensaje);
            email.IsBodyHtml = esHtml;
            cliente.Send(email);
            email.Dispose();
        }
    }
}
