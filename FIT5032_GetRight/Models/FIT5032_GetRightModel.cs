using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_GetRight.Models
{
    public partial class FIT5032_GetRightModel : DbContext
    {
        public FIT5032_GetRightModel()
            : base("name=FIT5032_GetRightModel")
        {
        }

        public virtual DbSet<Dieter> Dieters { get; set; }
        public virtual DbSet<DietHistory> DietHistories { get; set; }
        public virtual DbSet<Gym> Gyms { get; set; }
        public virtual DbSet<MealList> MealLists { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dieter>()
                .HasMany(e => e.DietHistories)
                .WithRequired(e => e.Dieter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dieter>()
                .HasMany(e => e.MealLists)
                .WithRequired(e => e.Dieter)
                .WillCascadeOnDelete(false);
            /*
            modelBuilder.Entity<Gym>()
                .HasMany(e => e.Trainers)
                .WithRequired(e => e.Gym)
                .WillCascadeOnDelete(false);
            */
        }
    }
}
