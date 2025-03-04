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
        public List<CommentViewModel>? Replies { get; set; } = new List<CommentViewModel>();
    }
}
