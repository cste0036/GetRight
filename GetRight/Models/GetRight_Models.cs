using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GetRight.Models
{
    public partial class GetRight_Models : DbContext
    {
        public GetRight_Models()
            : base("name=GetRight_Models")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Dieter> Dieters { get; set; }
        public virtual DbSet<DietHistory> DietHistories { get; set; }
        public virtual DbSet<Gym> Gyms { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<MealList> MealLists { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dieter>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Dieter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dieter>()
                .HasMany(e => e.DietHistories)
                .WithRequired(e => e.Dieter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dieter>()
                .HasMany(e => e.MealLists)
                .WithRequired(e => e.Dieter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gym>()
                .Property(e => e.Latitude)
                .HasPrecision(10, 8);

            modelBuilder.Entity<Gym>()
                .Property(e => e.Longitude)
                .HasPrecision(11, 8);

            modelBuilder.Entity<Gym>()
                .HasMany(e => e.Trainers)
                .WithRequired(e => e.Gym)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Newsletter>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Newsletter>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Trainer>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Trainer)
                .WillCascadeOnDelete(false);
        }
    }
}
