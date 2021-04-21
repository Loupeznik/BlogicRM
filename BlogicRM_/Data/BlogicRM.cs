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
        public DbSet<ContractAdvisor> contractAdvisor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Advisor>().ToTable("Advisor");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Institution>().ToTable("Institution");
            modelBuilder.Entity<ContractAdvisor>().ToTable("ContractAdvisor");

            modelBuilder.Entity<ContractAdvisor>()
                .HasKey(c => new { c.ContractID, c.AdvisorID });

        }

    }
}
