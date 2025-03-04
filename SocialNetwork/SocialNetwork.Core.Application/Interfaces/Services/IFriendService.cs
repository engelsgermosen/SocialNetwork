using SocialNetwork.Core.Application.ViewModel.Friend;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendService : IGenericService<FriendViewModel, SaveFriendViewModel,Friend>
    {
        Task<List<FriendViewModel>> GetAllWithIncludes();

        Task DeleteFriend(int id);
    }
}
