using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.Friend;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class FriendService : GenericService<FriendViewModel, SaveFriendViewModel, Friend>, IFriendService
    {
        private readonly IFrindRepository _friendRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IUserService userService;

        public FriendService(IFrindRepository friendRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService) : base(friendRepository, mapper)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            this.userService = userService;
        }

        public override async Task<SaveFriendViewModel> CreateAsync(SaveFriendViewModel vm)
        {
            var amigo = await userService.GetByUsername(vm.Username);

            if (amigo == null)
                return null;

            Friend friend = new()
            {
                Date = DateTime.Now,
                FriendId = amigo.Id,
                UserId = userViewModel.Id,
            };
            var response = await _friendRepository.AddAsync(friend);

            return _mapper.Map<SaveFriendViewModel>(response);
        }

        public async Task<List<FriendViewModel>> GetAllWithIncludes()
        {
            var response = await _friendRepository.GetAllWithIncludesAsync(new List<string> { "FriendUser", "User" });

            return response
                .Where(x => x.UserId == userViewModel.Id).ToList()
                .Select(friend => new FriendViewModel
                {
                    Id = friend.FriendId ?? 0,
                    Name = friend.FriendUser.Name,
                    Lastname = friend.FriendUser.Lastname,
                    Username = friend.FriendUser.Username,
                    ImagePath = friend.FriendUser.ImagePath,
                })
                .ToList();
        }

        public async Task DeleteFriend(int id)
        { 
            var response = await _friendRepository.DeleteFriend(userViewModel.Id, id);
            await _friendRepository.DeleteAsync(response);
        }
    }
}
