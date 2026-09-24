using System.Security.Cryptography.X509Certificates;

namespace GoalApp
{
    public class Goal // Base class for goals
    {
        public string Name { get; set; }
        public string HowOften { get; set; }
        public string ReminderFrequency { get; set; }
        public bool IsCompleted { get; set; }
        public Goal(string name, string howOften, string frequency) //base constructor to initialize goal properties
        {
            Name = name;
            HowOften = howOften;
            ReminderFrequency = frequency;
            IsCompleted = false;
        }

        public virtual void DisplayGoal() // Virtual method to display goal details
        {
            Console.WriteLine($"Goal: {Name}");
            Console.WriteLine($"How Often: {HowOften}");
            Console.WriteLine($"Reminder Frequency: {ReminderFrequency}");
            Console.WriteLine($"Completed: {IsCompleted}");
        }
        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }
    }
    public class HabitBreakGoal : Goal // Specific Goal Type that inherits from Goal
    {
        public int DaysSinceHabitRelapse { get; set; }
        public HabitBreakGoal(string name, string howOften, string frequency) 
            : base(name, howOften, frequency)
        {
            DaysSinceHabitRelapse = 0;
        }
        public override void DisplayGoal() //overrides the DisplayGoal method to include DaysSinceHabitRelapse.
                                           //used Claude AI to help with formatting and code structure.
        {
            base.DisplayGoal();
            Console.WriteLine($"Days Since Habit Relapse: {DaysSinceHabitRelapse}");
        }
    }

    public class HabitMakeGoal : Goal //Inherits from Goal
    {
        public int HabitStreak { get; set; }
        public HabitMakeGoal(string name, string howOften, string frequency) 
            : base(name, howOften, frequency)
        {
            HabitStreak = 0;
        }
        public override void DisplayGoal() //overrides the DisplayGoal method to include HabitStreak
        {
            base.DisplayGoal();
            Console.WriteLine($"Habit Streak: {HabitStreak}");
        }
    }
    public class ExerciseGoal : Goal // Inherits from Goal
    {
        public List<(string ExerciseType, int Reps)> Exercises { get; set; }

        public ExerciseGoal(string name, string howOften, string frequency)
            : base(name, howOften, frequency)
        {
            Exercises = new List<(string, int)>();
        }

        // Overload 1: add a single exercise with reps
        //used Claude AI to help with formatting and code structure.
        public void AddExercise(string exerciseType, int reps)
        {
            Exercises.Add((exerciseType, reps));
        }

        // Overload 2: add a single exercise with no reps specified (defaults to 0)
        public void AddExercise(string exerciseType)
        {
            Exercises.Add((exerciseType, 0));
        }

        // Overload 3: add several exercises at once, all with the same reps
        public void AddExercise(int reps, params string[] exerciseTypes) // Params allows for a variable number of arguments
        {
            foreach (var type in exerciseTypes)
            {
                Exercises.Add((type, reps));
            }
        }

        // Overload 4: add a batch of exercises that already have individual rep counts
        public void AddExercise(List<(string ExerciseType, int Reps)> exercisesToAdd)
        {
            Exercises.AddRange(exercisesToAdd); //AddRange adds all elements of the provided list to the Exercises list
        }

        public override void DisplayGoal()
        {
            base.DisplayGoal();
            Console.WriteLine("Exercises:");
            foreach (var ex in Exercises)
            {
                Console.WriteLine($"  - {ex.ExerciseType}: {ex.Reps} reps");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }
}