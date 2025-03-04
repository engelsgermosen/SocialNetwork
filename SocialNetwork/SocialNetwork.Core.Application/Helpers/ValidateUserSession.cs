using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.ViewModel.User;

namespace SocialNetwork.Core.Application.Helpers
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public bool HasUser()
        {
            UserViewModel user = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            return user != null;
        }
    }
}
