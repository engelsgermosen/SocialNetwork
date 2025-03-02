﻿using SocialNetwork.Core.Application.ViewModel.Comments;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<CommentViewModel, SaveCommentViewModel, Comments>
    {
    }
}
