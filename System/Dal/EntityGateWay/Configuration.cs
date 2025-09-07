using DataModel.Models.DataBase;
using EntityGateWay.Repository.Implementations;
using EntityGateWay.Repository.Interfaces;
using EntityGateWay.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityGateWay
{
    public static class Configuration
    {
        public static void ConfigurationDBContext(IServiceCollection collection, string connectionStr)
        {
            _ = collection.AddDbContext<BonchiDBContext>(options => options.UseNpgsql(connectionStr));
        }

        public static void ConfigurationRepository(IServiceCollection collection)
        {
            collection.AddScoped<IRepository<User, Guid>>();
            collection.AddScoped<IRepository<Workout, Guid>>();
            collection.AddScoped<IRepository<Exercise, Guid>>();
            collection.AddScoped<IRepository<WorkoutExercise, long>>();

            collection.AddScoped<IUserRepository, UserRepository>();
            collection.AddScoped<IWorkoutRepository, WorkoutRepository>();
            collection.AddScoped<IExerciseRepository, ExerciseRepository>();
            collection.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
        }
    }
}