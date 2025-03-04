using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModel.User
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="El nombre de usuario es obligatorio")]

        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }
}
