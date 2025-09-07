using DataModel.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EntityGateWay
{
    internal partial class BonchiDBContext
    {
        private void BindingTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Workouts)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany()
                .HasForeignKey(we => we.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany()
                .HasForeignKey(we => we.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutExercise>()
                .HasIndex(we => new { we.WorkoutId, we.ExerciseId })
                .IsUnique(false);
        }

        private void ConfigureIdProperty(ModelBuilder modelBuilder, Type entityType, Type idType)
        {
            if (idType == typeof(long))
            {
                modelBuilder.Entity(entityType)
                    .Property("Id")
                    .ValueGeneratedOnAdd();
            }
            else if (idType == typeof(Guid))
            {
                modelBuilder.Entity(entityType)
                    .Property("Id")
                    .HasDefaultValueSql("gen_random_uuid()");
            }
        }
    }
}