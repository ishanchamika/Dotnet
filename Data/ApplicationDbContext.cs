using Microsoft.EntityFrameworkCore;
using Register.Models.Entities;

namespace Register.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Signup> Signups { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionMap> QuestionMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensuring the foreign key relationship for QuestionMap -> Question
            modelBuilder.Entity<QuestionMap>()
                .HasOne(qm => qm.Question)
                .WithMany()  // No navigation property in Question
                .HasForeignKey(qm => qm.Current_qid)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensuring the foreign key relationship for Question -> Component
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Component)
                .WithMany()
                .HasForeignKey(q => q.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensuring the foreign key relationship for QuestionMap -> Component
            modelBuilder.Entity<QuestionMap>()
                .HasOne(qm => qm.Component)
                .WithMany()
                .HasForeignKey(qm => qm.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

