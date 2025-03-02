using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsername(string username);
        Task<User> GetByToken(string token);

        Task<User> Login(LoginViewModel vm);
    }
}
