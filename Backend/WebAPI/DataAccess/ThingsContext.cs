using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Backend.WebAPI.DataAccess
{
    public class ThingsContext : DbContext
    {
        public ThingsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(100);

            modelBuilder.Entity<Category>()
                .Property(c => c.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            modelBuilder.Entity<Person>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.PhoneNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<Person>()
                .Property(p => p.Email)
                .HasMaxLength(50);


            modelBuilder.Entity<Thing>()
                .Property(t => t.Description)
                .HasMaxLength(100);

            modelBuilder.Entity<Thing>()
                .Property(t => t.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Thing>()
                .HasOne(t => t.Category);

            modelBuilder.Entity<Loan>()
                .Property(l => l.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Loan>()
                .Property(l => l.ReturnDate);

            modelBuilder.Entity<Loan>()
                .Property(l => l.Status);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Person).WithMany();

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Thing).WithMany();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
