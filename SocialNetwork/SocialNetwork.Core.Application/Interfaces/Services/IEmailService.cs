using SocialNetwork.Core.Application.Dtos.Email;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
