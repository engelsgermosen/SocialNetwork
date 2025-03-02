namespace SocialNetwork.Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ImagePath { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = false;

        public string? TokenActive { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public ICollection<Friend>? Friends { get; set; }
        public ICollection<Friend>? FriendsOf { get; set; }
        public ICollection<Comments>? Comments { get; set; }


    }
}
