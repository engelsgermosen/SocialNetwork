namespace SocialNetwork.Core.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public string? ImagePath { get; set; }
        public string? VideoUrl { get; set; }

        public DateTime Date {get;set;} = DateTime.Now;

        public ICollection<Comments>? Comments { get; set; }

        public User? User { get; set; }

        public int UserId { get; set; }
    }
}
