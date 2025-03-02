using SocialNetwork.Core.Application.ViewModel.Friend;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendService : IGenericService<FriendViewModel, SaveFriendViewModel,Friend>
    {
        Task<List<FriendViewModel>> GetAllWithIncludes();

        Task DeleteFriend(int id);
    }
}
