using ExcerciseApp.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExcerciseApp.API.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<BaseExcercise> BaseExcercises { get; set; }
        public DbSet<WeightLiftingExcercise> WeightLiftingExcercises { get; set; }
        public DbSet<CardioExcercise> CardioExcercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Workout>().ToTable("workouts");
            modelBuilder.Entity<Workout>().HasKey(p => p.WorkoutId);
            modelBuilder.Entity<Workout>()
                .HasMany(p => p.Excercises)
                .WithOne(p => p.Workout)
                .HasForeignKey(p => p.WorkoutId);

            modelBuilder.Entity<BaseExcercise>().HasKey(p => p.Id);
        }
    }
}

