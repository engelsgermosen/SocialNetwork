using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModel.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Las contraseñas deben coincidir")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Upload)]

        public IFormFile? Image { get; set; }

        public string? TokenActive { get; set; }

        public string? ImagePath { get; set; }
        public bool IsActive { get; set; } 


    }
}
