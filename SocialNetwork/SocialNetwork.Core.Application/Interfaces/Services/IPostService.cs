using SocialNetwork.Core.Application.ViewModel.Post;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<PostViewModel, SavePostViewModel,Post>
    {
        Task<List<PostViewModel>> GetAllFriendsPostViewModel();

        Task<List<PostViewModel>> GetAllNewPostViewModels();
    }
}
