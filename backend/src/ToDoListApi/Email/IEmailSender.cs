using System.Threading.Tasks;
using ToDoListApi.Entities;

namespace ToDoListApi.Email
{
    public interface IEmailSender
    {
        Task<EmailResponse> SendEmailAsync(AppUser user, string token);
    }
}