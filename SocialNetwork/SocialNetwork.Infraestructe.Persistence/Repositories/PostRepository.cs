using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;


namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationContext _context;

        public PostRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<List<Post>> GetPostAsync()
        {
            return await _context.Set<Post>()
                .Include(x => x.User)
                .Include(x => x.Comments)
                    .ThenInclude(c => c.User)
                .Include(x => x.Comments)
                    .ThenInclude(c => c.Replies)
                    .ThenInclude(r => r.User)
                .ToListAsync();
        }
    }
}
