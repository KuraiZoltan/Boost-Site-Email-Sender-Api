using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using EmailSender.Models;

namespace EmailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail([FromBody] OrderDetails orderDetails)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("fakebboost@gmail.com"));
            email.To.Add(MailboxAddress.Parse(orderDetails.Email));
            email.Subject = "FB Order Confirmation";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text =
                $"<h1>FB Order Details</h1> " +
                $"<p>First Name: {orderDetails.FirstName}</p> " +
                $"<p>Last Name: {orderDetails.LastName}</p> " +
                $"<p>Boost From: {orderDetails.CurrentRank} {orderDetails.CurrentRankLevel}</p> " +
                $"<p>Boost To: {orderDetails.OrderedRank} {orderDetails.OrderedRankLevel}</p>" +
                "<h1>Thank You for your order, FB will contact you soon!</h1>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("fakebboost@gmail.com", "gconlzshnpusjupa");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
