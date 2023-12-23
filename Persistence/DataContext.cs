using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Entrepreneur> Entrepreneurs { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Education>()
                .HasOne(x => x.User)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Experience>()
                .HasOne(x => x.User)
                .WithMany(u => u.Experiences)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Skill>()
                .HasOne(x => x.User)
                .WithMany(u => u.Skills)
                .HasForeignKey(e => e.UserId);

        

        }


    }
}