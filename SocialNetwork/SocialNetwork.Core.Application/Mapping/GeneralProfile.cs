using AutoMapper;
using SocialNetwork.Core.Application.ViewModel.Comments;
using SocialNetwork.Core.Application.ViewModel.Friend;
using SocialNetwork.Core.Application.ViewModel.Post;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, UserViewModel>()
                .ReverseMap()
                .ForMember(x => x.TokenActive, opt => opt.Ignore())
                .ForMember(x => x.Posts, opt => opt.Ignore())
                .ForMember(x => x.Friends, opt => opt.Ignore())
                .ForMember(x => x.FriendsOf, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore());

            CreateMap<User, SaveUserViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Posts, opt => opt.Ignore())
                .ForMember(x => x.Friends, opt => opt.Ignore())
                .ForMember(x => x.FriendsOf, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore());

            CreateMap<Post, PostViewModel>()
                .ReverseMap()
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<Post, SavePostViewModel>()
                .ReverseMap()
                .ForMember(x => x.User, opt => opt.Ignore());


            CreateMap<Friend, SaveFriendViewModel>()

                .ReverseMap()
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.FriendUser, opt => opt.Ignore())
                .ForMember(x => x.Date, opt => opt.Ignore())
                .ForMember(x => x.FriendId, opt => opt.Ignore());

            CreateMap<User, UpdateUserViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Posts, opt => opt.Ignore())
                .ForMember(x => x.Friends, opt => opt.Ignore())
                .ForMember(x => x.FriendsOf, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.TokenActive, opt => opt.Ignore())
                .ForMember(x => x.ImagePath, opt => opt.Ignore());



            CreateMap<Comments, CommentViewModel>()
                .ForMember(x => x.Publication, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Post, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<Comments, SaveCommentViewModel>()
                .ReverseMap()
                .ForMember(x => x.Post, opt => opt.Ignore())
                .ForMember(x => x.Replies, opt => opt.Ignore())
                .ForMember(x => x.ParentComment, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());


        }
    }
}
