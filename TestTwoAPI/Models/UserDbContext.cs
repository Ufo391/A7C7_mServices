using Microsoft.EntityFrameworkCore;

namespace TestUserService.Models
{
    public class UserDbContext : DbContext
    {
        private const string connectionString = @"
            Data Source=127.0.0.1,1433;
            Initial Catalog=TestUsers;
            Persist Security Info=True;
            User ID=sa;
            Password=Test_Password123;
            MultipleActiveResultSets=True";

        public UserDbContext() { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
