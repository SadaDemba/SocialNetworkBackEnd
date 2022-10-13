using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Tools;
using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Data
{
    public class SNDataContext:DbContext
    {

        public SNDataContext(DbContextOptions<SNDataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        // public SNDataContext()
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasMany(u => u.Publications).
                WithOne(p => p.User);

            modelBuilder.Entity<Publication>().
                HasMany(p => p.Comments).
                WithOne(c => c.Publication);

/*            modelBuilder.Entity<User>().
                HasMany(u => u.Likes).WithOne(l =>l.User).OnDelete(DeleteBehavior.ClientNoAction);*/


            modelBuilder.Entity<Publication>().
               HasMany(p => p.Likes).WithOne(l => l.Publication);

        }
    }
}
