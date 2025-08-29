using LayerUseCase.Interface;
using System.Net;
using System.Net.Mail;

namespace LayerService;

public class ReceiveEmail : IRecibirCorreo
{

    public async Task<bool> RecibirCorreo(string correo, string asunto, string mensaje)
    {
        bool resultado = false;
        try
        {
            using (var mail = new MailMessage())
            {
                mail.To.Add(correo);
                mail.From = new MailAddress("cesarcerdacomputer@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;
                mail.Headers.Add("X-Priority", "1"); // Alta prioridad
                mail.Headers.Add("X-MSMail-Priority", "High");
                mail.Headers.Add("Importance", "High");

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("cesarcerdacomputer@gmail.com", "vjqhpeaglpjsmhuy");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            resultado = true;
        }
        catch (Exception e)
        {
            // Registra o muestra el mensaje de error
            Console.WriteLine($"Error al enviar correo: {e.Message}");
        }
        return resultado;
    }
}
