using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : GenericService<UserViewModel, SaveUserViewModel, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserViewModel userVm;


        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            userVm = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<UserViewModel> GetByUsername(string username)
        {
            var response = await _userRepository.GetByUsername(username);
            return _mapper.Map<UserViewModel>(response);
        }

        public override async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            return await base.GetByIdSaveViewModel(id);
        }

        public override async Task<SaveUserViewModel> UpdateAsync(SaveUserViewModel vm, int id)
        {
            var userOlg = await _userRepository.GetByIdAsync(id);

            if(string.IsNullOrWhiteSpace(vm.Password))
            {
                vm.Password = userOlg.Password;
            }
            else
            {
                vm.Password = PasswordEncrypter.ComputeHash(vm.Password);
            }
            
            return await base.UpdateAsync(vm, id);
        }

        public async Task<UpdateUserViewModel> UpdateUserPerfil(UpdateUserViewModel vm)
        {
            var response = await _userRepository.GetByIdAsync(vm.Id);


            if(!string.IsNullOrWhiteSpace(vm.Password))
            {
                response.Password = PasswordEncrypter.ComputeHash(vm.Password);
            }
            if(!string.IsNullOrWhiteSpace(vm.ImagePath))
            {
                response.ImagePath = vm.ImagePath;
            }

            response.Name = vm.Name;
            response.Email = vm.Email;
            response.Phone = vm.Phone;
            response.Lastname = vm.Lastname;

            response = await _userRepository.UpdateAsync(response, response.Id);
                
            return _mapper.Map<UpdateUserViewModel>(response);
        }

        public async Task<UpdateUserViewModel> GetUserInSession()
        {
            var response = await _userRepository.GetByIdAsync(userVm.Id);
            return _mapper.Map<UpdateUserViewModel>(response);
        }

        public async Task<UserViewModel> Login(LoginViewModel loginViewModel)
        {
            User user = await _userRepository.Login(loginViewModel);

            if (user == null)
                return null;
            if (!user.IsActive)
                return new UserViewModel() { IsActive=false};

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> PutUserActive(string token)
        {
            User usuario = await _userRepository.GetByToken(token);

            if (usuario == null)
                return null;

            usuario.IsActive = true;
            usuario.TokenActive = null;
            await _userRepository.UpdateAsync(usuario, usuario.Id);
            return _mapper.Map<UserViewModel>(usuario);
        }

        public async Task<UserResetPassword> ResetPassword(string username)
        {
            var user = await _userRepository.GetByUsername(username);

            if (user == null)
                return null;

            user.Password = Guid.NewGuid().ToString("N").Substring(0, 12);

            var updatedUser = new UserResetPassword()
            {
                Email = user.Email,
                Password = user.Password,
            };

            user.Password = PasswordEncrypter.ComputeHash(updatedUser.Password);
            await _userRepository.UpdateAsync(user, user.Id);

            return  updatedUser;
        }
    }
}
