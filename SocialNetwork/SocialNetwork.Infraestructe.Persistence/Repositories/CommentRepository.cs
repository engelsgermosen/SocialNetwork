using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infraestructure.Persistence.Contexts;

namespace SocialNetwork.Infraestructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comments>, ICommentRepository
    {
        private readonly ApplicationContext _context;

        public CommentRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _context = applicationContext;
        }
    }
}
