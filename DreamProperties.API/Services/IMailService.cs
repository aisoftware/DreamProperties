using DreamProperties.API.Models;
using System.Threading.Tasks;

namespace DreamProperties.API.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
