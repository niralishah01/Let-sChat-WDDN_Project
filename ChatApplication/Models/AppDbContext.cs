using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<friend>()
                .HasOne<ApplicationUser>(f => f.user)
                .WithMany(f => f.friends)
                .HasForeignKey("userID");

            builder.Entity<messege>()
                .HasOne<ApplicationUser>(m => m.user)
                .WithMany(m => m.messeges)
                .HasForeignKey("senderID");
        }
        public DbSet <ApplicationUser> AspNetUsers { get; set; }
        public DbSet<friend> friends { get; set; }
        public DbSet<messege> messeges { get; set; }
    }
}
