using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModel.Friend
{
    public class SaveFriendViewModel
    {
        [Required(ErrorMessage ="El nombre de usuario es obligatorio")]
        [DataType(DataType.Text)]
        public string Username {  get; set; }

        public int UserId { get; set; }
    }
}
