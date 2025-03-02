
namespace SocialNetwork.Core.Domain.Entities
{
    public class Comments
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public Post? Post { get; set; }

        public int PostId { get; set; }

        public ICollection<Comments>? Replies { get; set; } = new List<Comments>();

        public int? ParentCommentId { get; set; }

        public Comments? ParentComment { get; set; }

        public User? User { get; set; }

        public int? UserId { get; set; }
    }
}
