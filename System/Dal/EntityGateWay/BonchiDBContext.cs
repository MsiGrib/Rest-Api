using DataModel.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EntityGateWay
{
    internal partial class BonchiDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutsExercises { get; set; }

        public BonchiDBContext(DbContextOptions<BonchiDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BindingTable(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var entityClrType = entityType.ClrType;
                var entityInterface = entityClrType.GetInterface("IEntity`1");

                if (entityInterface != null)
                {
                    var idProperty = entityType.FindProperty("Id");
                    if (idProperty != null)
                        ConfigureIdProperty(modelBuilder, entityClrType, idProperty.ClrType);
                }
            }
        }
    }
}