using SocialNetwork.Core.Application.ViewModel.Comments;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<CommentViewModel, SaveCommentViewModel, Comments>
    {
    }
}
