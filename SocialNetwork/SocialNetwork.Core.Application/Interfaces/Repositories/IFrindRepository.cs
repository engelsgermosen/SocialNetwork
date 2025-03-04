using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IFrindRepository : IGenericRepository<Friend>
    {
        Task<Friend> DeleteFriend(int userId,int FriendId);
    }
}
