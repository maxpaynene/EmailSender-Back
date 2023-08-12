using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace EmailApi.Controllers
{
    [Route("api/email-sender")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] Models.EmailForm formData)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.mailgun.org")
                {
                    Port = 587,
                    // TODO: encripte password
                    Credentials = new NetworkCredential("postmaster@sandboxc4f9463ef84a4f4fa8e1889658c5a0e5.mailgun.org", "895425a4e4c344d11895cc3dbddc0527-28e9457d-ecad2426"),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("brad@sandboxc4f9463ef84a4f4fa8e1889658c5a0e5.mailgun.org"),
                    Subject = "Proyect sender mail",
                    Body = $"Nombre: {formData.Name}\n Rut: {formData.Rut}\n Email: {formData.Email}",
                    IsBodyHtml = false,

                };

                //TODO: find server smtp free that support attachment
                /*if (formData.PdfFile != null && formData.PdfFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formData.PdfFile.CopyToAsync(stream);
                        var attachment = new Attachment(stream, formData.PdfFile.FileName, "application/pdf");
                        mailMessage.Attachments.Add(attachment);
                    }
                }*/

                mailMessage.To.Add(formData.Email);

                await smtpClient.SendMailAsync(mailMessage);

                return Ok("{status: 200}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error exception: {ex.Message}");
            }
        }
    }
}
