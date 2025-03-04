using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.Comments;
using SocialNetwork.Core.Application.ViewModel.Post;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class PostService : GenericService<PostViewModel, SavePostViewModel, Post>, IPostService
    {
        private readonly IPostRepository _postRespository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserViewModel userVM;
        private readonly IFriendService friendService;



        public PostService(IPostRepository postRespository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IFriendService friendService) : base(postRespository, mapper)
        {
            _postRespository = postRespository;
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            userVM = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            this.friendService = friendService;
        }

        public async Task<List<PostViewModel>> GetAllNewPostViewModels()
        {
            var posts = await _postRespository.GetPostAsync();

            return posts
                .Where(x => x.UserId == userVM.Id).OrderByDescending(x => x.Date)
                .Select(x => new PostViewModel
                {

                    Id = x.Id,
                    Content = x.Content,
                    Date = x.Date,
                    VideoUrl = x.VideoUrl,
                    ImagePath = x.ImagePath,
                    UserId = x.UserId,
                    UserImagePath = x?.User?.ImagePath,
                    Username = x?.User?.Username,
                    Comments = x?.Comments?.Select(n => new CommentViewModel
                    {
                        Id = n.Id,
                        Message = n.Message,
                        PostId = n.PostId,
                        ImagePath = n?.User?.ImagePath,
                        Created = n.Created,
                        UserId = n?.UserId,
                        ParentCommentId = n?.ParentCommentId,
                        Replies = x?.Comments?.Where(c => c.ParentCommentId == n?.Id)
                        .Select(coc => new CommentViewModel
                        {
                            Id = coc.Id,
                            Message = coc.Message,
                            Created = coc.Created,
                            PostId = coc.PostId,
                            UserId = coc.UserId,
                            ImagePath = coc.User?.ImagePath,
                            ParentCommentId = coc.ParentCommentId,
                        })
                        .ToList(),
                    }).ToList(),
                })
                .ToList();
        }

        public override async Task<SavePostViewModel> CreateAsync(SavePostViewModel vm)
        {
            vm.UserId = userVM.Id;
            return await base.CreateAsync(vm);
        }

        public async Task<List<PostViewModel>> GetAllFriendsPostViewModel()
        {
            var response = await _postRespository.GetPostAsync();
            var amigos = await friendService.GetAllWithIncludes();
            var result = response
                .Where(x => amigos.Any(a => a.Id == x.UserId))
                .OrderByDescending(x => x.Date)
                .ToList();

            return result
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    Date = x.Date,
                    VideoUrl = x.VideoUrl,
                    ImagePath = x.ImagePath,
                    UserId = x.UserId,
                    UserImagePath = x?.User?.ImagePath,
                    Username = x?.User?.Username,
                    Comments = x?.Comments?.Select(n => new CommentViewModel
                    {
                        Id = n.Id,
                        Message = n.Message,
                        PostId = n.PostId,
                        ImagePath = n?.User?.ImagePath,
                        Created = n.Created,
                        UserId = n?.UserId,
                        ParentCommentId = n?.ParentCommentId,
                        Replies = x?.Comments?.Where(c => c.ParentCommentId == n?.Id)
                        .Select(coc => new CommentViewModel
                        {
                            Id = coc.Id,
                            Message = coc.Message,
                            Created = coc.Created,
                            PostId = coc.PostId,
                            UserId = coc.UserId,
                            ImagePath = coc.User?.ImagePath,
                            ParentCommentId = coc.ParentCommentId,
                        })
                        .ToList(),
                    }).ToList(),
                }).ToList();
        }
    }
}
