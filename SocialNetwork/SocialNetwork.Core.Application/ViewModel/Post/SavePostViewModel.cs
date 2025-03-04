using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModel.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string? Content { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }

        [DataType(DataType.Text)]
        public string? VideoUrl { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}
