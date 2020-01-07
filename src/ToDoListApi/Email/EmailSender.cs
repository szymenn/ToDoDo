using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Options;
using Constants = ToDoListApi.Helpers.Constants;

namespace ToDoListApi.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailVerificationSettings> _emailSettings;

        public EmailSender(IOptions<EmailVerificationSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }
        
        public async Task<EmailResponse> SendEmailAsync(AppUser user, string token)
        {
            var client = new SendGridClient(_emailSettings.Value.ApiKey);
            var from = new EmailAddress(_emailSettings.Value.FromEmail, _emailSettings.Value.FromName);
            var to = new EmailAddress(user.Email, user.Email);
            var content = $"<a href={Constants.ApiUrl}/user/email/verify" +
                          $"?userId={HttpUtility.UrlEncode(user?.Id)}" +
                          $"&confirmationToken={HttpUtility.UrlEncode(token)}>Verify</a>";
            var msg = MailHelper.CreateSingleEmail(
                from, 
                to, 
                _emailSettings.Value.Subject, 
                null,
                content);
            
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return new EmailResponse();

            }
            
            throw new EmailSenderException(Constants.EmailSenderException);
        }
    }
}