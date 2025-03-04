using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Infraestructure.Persistence.Contexts;
using SocialNetwork.Infraestructure.Persistence.Repositories;

namespace SocialNetwork.Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), a => a.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFrindRepository, FriendRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
        }
    }
}
