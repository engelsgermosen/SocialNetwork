using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class FriendRepository : GenericRepository<Friend>, IFrindRepository
    {
        private readonly ApplicationContext _context;

        public FriendRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<Friend> DeleteFriend(int userId, int FriendId)
        {
            return await _context.Set<Friend>().FirstOrDefaultAsync(x => x.UserId == userId && x.FriendId == FriendId);
        }
    }
}
