using SocialNetwork.Core.Application.ViewModel.Post;


namespace SocialNetwork.Core.Application.ViewModel.Comments
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? ImagePath { get; set; }

        public DateTime Created { get; set; }
        public int? UserId { get; set; }
        public int? ParentCommentId { get; set; }
        public int PostId { get; set; }
        public PostViewModel? Publication { get; set; }
        public CommentViewModel? Parent { get; set; }
        public List<CommentViewModel>? ParentComment { get; set; }
    }
}
