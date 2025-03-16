using Microsoft.EntityFrameworkCore;

namespace NetigentTest.Models.DBModels
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<StatusLevel> StatusLevels { get; set; }
        public DbSet<AppProject> AppProjects { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppProject>()
                .HasOne(a => a.StatusLevel)
                .WithMany(s => s.AppProjects)
                .HasForeignKey(a => a.StatusId);

            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.AppProject)
                .WithMany(a => a.Inquiries)
                .HasForeignKey(i => i.AppProjectId);
        }
    }
}
