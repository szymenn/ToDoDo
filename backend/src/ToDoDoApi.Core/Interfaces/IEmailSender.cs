using System.Threading.Tasks;
using ToDoDoApi.Core.Dtos;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Core.Interfaces
{
    public interface IEmailSender
    {
        Task<EmailResponse> SendEmailAsync(AppUser user, string token);
    }
}