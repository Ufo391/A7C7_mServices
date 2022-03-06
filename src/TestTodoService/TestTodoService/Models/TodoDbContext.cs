using Microsoft.EntityFrameworkCore;

namespace TestTodoService.Models
{
    public class TodoDbContext : DbContext
    {
        /*private const string connectionString = @"
            Data Source=127.0.0.1,1433;
            Initial Catalog=TestTodos;
            Persist Security Info=True;
            User ID=sa;
            Password=Test_Password123;
            MultipleActiveResultSets=True";
        */

        public TodoDbContext() { }

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<Todo> Todos { get; set; }
    }
}
