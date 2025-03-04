using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModel.Post
{
    public class EditPostViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Content { get; set; }

        public IFormFile Image { get; set; }
        public string? ImagePath { get; set; }
        public string? VideoUrl { get; set; }
    }
}
