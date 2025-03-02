using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.Comments;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : GenericService<CommentViewModel, SaveCommentViewModel, Comments>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly UserViewModel userViewModel;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(commentRepository,mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SaveCommentViewModel> CreateAsync(SaveCommentViewModel vm)
        {
            vm.Created = DateTime.Now;
            vm.UserId = userViewModel?.Id;
            return await base.CreateAsync(vm);
        }
    }
}
