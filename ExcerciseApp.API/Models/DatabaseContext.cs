using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExcerciseApp.API.Models
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<BaseExcercise> BaseExcercises { get; set; }
        public DbSet<WeightLiftingExcercise> WeightLiftingExcercises { get; set; }
        public DbSet<CardioExcercise> CardioExcercises { get; set; }
    }
}

