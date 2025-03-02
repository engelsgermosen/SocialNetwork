using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
