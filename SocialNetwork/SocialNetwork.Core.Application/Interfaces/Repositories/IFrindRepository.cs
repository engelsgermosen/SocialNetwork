using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IFrindRepository : IGenericRepository<Friend>
    {
        Task<Friend> DeleteFriend(int userId,int FriendId);
    }
}
