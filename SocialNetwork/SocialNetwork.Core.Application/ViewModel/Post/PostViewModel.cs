using SocialNetwork.Core.Application.ViewModel.Comments;

namespace SocialNetwork.Core.Application.ViewModel.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public string? UserImagePath { get; set; }

        public string? Username { get; set; }
        public string? ImagePath { get; set; }

        public string? VideoUrl { get; set; }

        public DateTime Date { get; set; }

        public ICollection<CommentViewModel>? Comments { get; set; }

        public int UserId { get; set; }
    }
}
