using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Entity
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "test1",
                    Email = "test1@gmail.com",
                    Password = "test1tt",
                    ConfirmPwd = "test1tt",
                }
            );
        }
    }
}
