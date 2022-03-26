using El3edda.Data.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace OrderConfirmationEmailAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/{toEmail}")]
    [ApiController]
    public class SendMailerController : ControllerBase
    {
        private IMailService mailService;

        public SendMailerController(IMailService _mailService)
        {
            mailService = _mailService;
        }

        [System.Web.Http.HttpGet]
        public IActionResult SendEmail(string toEmail)
        {
            if (toEmail == null)
                return BadRequest();

            try
            {
                string path = @"wwwroot\EmailPage.htm";
                string body = System.IO.File.ReadAllText(path);

                MailRequest mailRequest = new MailRequest()
                {
                    Body = body,
                    Subject = "Order Confirmation",
                    ToEmail = toEmail
                };

                mailService.SendEmail(mailRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(504);
            }

            return Ok();
        }
    }
}
