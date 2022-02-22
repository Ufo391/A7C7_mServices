using Microsoft.EntityFrameworkCore;

namespace TestTwoAPI.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
    }
}
