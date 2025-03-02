using SocialNetwork.Core.Application.ViewModel.Post;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<PostViewModel, SavePostViewModel,Post>
    {
        Task<List<PostViewModel>> GetAllWithIncludesAsync();

        Task<List<PostViewModel>> GetAllFriendsPostViewModel();
    }
}
