using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModel.Comments
{
    public class SaveCommentViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        //public List<int>? Publication { get; set; }
        //public List<int>? Children { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

    }
}
