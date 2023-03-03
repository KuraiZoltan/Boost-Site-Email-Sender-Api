using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("fakebboost@gmail.com"));
            email.To.Add(MailboxAddress.Parse("kz1022@gmail.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("fakebboost@gmail.com", "gconlzshnpusjupa");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
