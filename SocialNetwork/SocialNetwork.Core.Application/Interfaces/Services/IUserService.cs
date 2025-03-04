using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserViewModel, SaveUserViewModel, User>
    {
        Task<UserResetPassword> ResetPassword(string username);

        Task<UserViewModel> Login(LoginViewModel loginViewModel);

        Task<UserViewModel> PutUserActive(string token);

        Task<UserViewModel> GetByUsername(string username);

        Task<UpdateUserViewModel> UpdateUserPerfil(UpdateUserViewModel vm);
    }
}
