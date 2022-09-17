using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApplication.Model{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext>options): base(options){

        }
        public DbSet<Exercise>?ExerciseList{get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>().Property(p=>p.Id).HasIdentityOptions(startValue:4);

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise{
                    Id=1,
                    Title="Kätekõverdused jala tõstega",
                    Description="Tavalised kätekõverdused korda mõõda jala tõstes",
                    Intensity=Exercise.ExerciseIntensity.Normal,
                    ReccommendedDurationInSeconds=40,
                    RecommendedTimeInSecondsBeforeExercise=10,
                    RecommendedTimeInSecondsAfterExercise=10
                },
                new Exercise{
                    Id=2,
                    Title="Slaalomhüpped",
                    Description="Kükist hüpped küljelt küljele",
                    Intensity=Exercise.ExerciseIntensity.High,
                    ReccommendedDurationInSeconds=40,
                    RecommendedTimeInSecondsBeforeExercise=10,
                    RecommendedTimeInSecondsAfterExercise=10,
                    RestTimeInstructions="Venita reie esikülge"
                },
                new Exercise{
                    Id=3,
                    Title="Alt läbi jooks",
                    Description="Toenglamangus jooksmine",
                    Intensity=Exercise.ExerciseIntensity.Normal,
                    ReccommendedDurationInSeconds=40,
                    RecommendedTimeInSecondsBeforeExercise=10,
                    RecommendedTimeInSecondsAfterExercise=10,
                }
            );
        }
    }
}