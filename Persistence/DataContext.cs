using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser, IdentityRole<Guid> ,Guid>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }
        // public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ResumeStorage> ResumeStorages { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<JobSeeker>().ToTable("JobSeekers");
            // modelBuilder.Entity<Recruiter>().ToTable("Recruiters");
            // modelBuilder.Entity<Entrepreneur>().ToTable("Entrepreneurs");


            //     modelBuilder.Entity<Education>()
            //         .HasOne(x => x.User)
            //         .WithMany(u => u.Educations)
            //         .HasForeignKey(e => e.UserId);

            //     modelBuilder.Entity<Experience>()
            //         .HasOne(x => x.User)
            //         .WithMany(u => u.Experiences)
            //         .HasForeignKey(e => e.UserId);

            //     modelBuilder.Entity<Skill>()
            //         .HasOne(x => x.User)
            //         .WithMany(u => u.Skills)
            //         .HasForeignKey(e => e.UserId);


            //     modelBuilder.Entity<Company>()
            //         .HasOne(x => x.Entrepreneur)
            //         .WithMany(c => c.Companies)
            //         .HasForeignKey(e => e.EntrepreneurId);

            //      modelBuilder.Entity<Recruiter>()
            //         .HasOne(r => r.Company)
            //         .WithMany(c => c.Recruiters)
            //         .HasForeignKey(e => e.CompanyId);

            //     modelBuilder.Entity<JobPost>()
            //         .HasOne(r => r.Recruiter)
            //         .WithMany(c => c.JobPosts)
            //         .HasForeignKey(e => e.RecruiterId);

            //     modelBuilder.Entity<EmailNotification>()
            //         .HasOne(r => r.Company)
            //         .WithMany(c => c.EmailNotifications)
            //         .HasForeignKey(e => e.CompanyId);

            //     modelBuilder.Entity<Application>()
            //         .HasOne(r => r.JobPost)
            //         .WithMany(c => c.Applications)
            //         .HasForeignKey(e => e.JobPostId);

            //     modelBuilder.Entity<Application>()
            //         .HasOne(r => r.JobSeeker)
            //         .WithMany(c => c.Applications)
            //         .HasForeignKey(e => e.JobSeekerId);

            //     modelBuilder.Entity<Application>()
            //         .HasOne(r => r.EmailNotification)
            //         .WithMany()
            //         .HasForeignKey(e => e.JobSeekerId);

        }
    }
}