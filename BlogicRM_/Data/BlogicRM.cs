using Microsoft.EntityFrameworkCore;
using BlogicRM_.Models;

namespace BlogicRM_.Data
{
    public class BlogicRM : DbContext
    {
        public BlogicRM(DbContextOptions<BlogicRM> options) : base(options) { }

        public DbSet<Client> Client { get; set; }
        public DbSet<Advisor> Advisor { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Institution> Institution { get; set; }

    }
}
