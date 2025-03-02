using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModel.User
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
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

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben coincidir")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        public string? ImagePath { get; set; }
    }
}
