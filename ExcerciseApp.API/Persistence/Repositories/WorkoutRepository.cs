using ExcerciseApp.API.Domain.Models;
using ExcerciseApp.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Persistence.Repositories
{
    public class WorkoutRepository : BaseRepository, IWorkoutRepository
    {
        public WorkoutRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(Workout workout)
        {
            await _context.Workouts.AddAsync(workout);
        }

        public void Delete(Workout workout)
        {
            _context.Workouts.Remove(workout);
        }

        public async Task<Workout> FindByIdAsync(int id)
        {
            return await _context.Workouts.FindAsync(id);
        }

        public async Task<IEnumerable<Workout>> ListAsync()
        {
            return await _context.Workouts.Include(w=>w.Excercises).ToListAsync();
        }

        public void Update(Workout workout)
        {
            _context.Workouts.Update(workout);
        }
    }
}
