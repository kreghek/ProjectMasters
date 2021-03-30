using Microsoft.EntityFrameworkCore;

using ProjectMasters.Models;

namespace ProjectMasters.Data
{
    public class ProjectMastersContext : DbContext
    {
        public ProjectMastersContext (DbContextOptions<ProjectMastersContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Project { get; set; }

        public DbSet<SkillScheme> SkillSchemes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SkillScheme>().HasData(new SkillScheme { Id = -1, Name = "Frontend" }, new SkillScheme { Id = -2, Name = "Backend" }, new SkillScheme { Id = -3, Name = "Mobile" });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProjectMasters.Models.Feature> Feature { get; set; }
    }
}
