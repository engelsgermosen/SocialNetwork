using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _context = applicationContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncrypter.ComputeHash(entity.Password);
            return await base.AddAsync(entity);
        }

        public async Task<User> GetByToken(string token)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.TokenActive == token);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> Login(LoginViewModel vm)
        {
            vm.Password = PasswordEncrypter.ComputeHash(vm.Password);
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Username == vm.Username && x.Password == vm.Password);
        }
    }
}
