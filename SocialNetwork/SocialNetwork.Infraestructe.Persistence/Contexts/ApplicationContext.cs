using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Infraestructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> db) : base(db) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<User>().ToTable("Usuarios");
            modelBuilder.Entity<Friend>().ToTable("Amigos");
            modelBuilder.Entity<Post>().ToTable("Publicaciones");
            modelBuilder.Entity<Comments>().ToTable("Comentarios");
            #endregion

            #region PrimaryKeys

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Friend>().HasKey(x => x.Id);
            modelBuilder.Entity<Post>().HasKey(x => x.Id);
            modelBuilder.Entity<Comments>().HasKey(x => x.Id);

            #endregion


            #region Configuration

            modelBuilder.Entity<User>()
                .Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username).IsUnique();




            #endregion


            #region foreign keys

            modelBuilder.Entity<User>()
                .HasMany<Post>(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friend>()
                .HasOne<User>(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne<User>(x => x.FriendUser)
                .WithMany(x => x.FriendsOf)
                .HasForeignKey(x => x.FriendId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Comments>()
                .HasOne<User>(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comments>()
                .HasOne<Post>(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>()
                .HasOne(x => x.ParentComment)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.ParentCommentId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

        }

    }
}
