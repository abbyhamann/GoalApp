using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Sources;

namespace GoalApp
{
    public delegate void GoalCompletedEventHandler(Goal goal); // Delegate for goal completion event
    public delegate void StreakMilestoneHandler(HabitMakeGoal goal, int streak); //Got idea from Claude AI to create a delegate for the HabitMakeGoal and HabitBreakGoal class to handle streak milestones
    public class Goal // Base class for goals
    {
        public string Name { get; set; }
        public string HowOften { get; set; }
        public string ReminderFrequency { get; set; }
        public bool IsCompleted { get; set; }
        public event GoalCompletedEventHandler OnCompleted; // Event to notify when a goal is completed

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
            OnCompleted?.Invoke(this); // Invoke the event if there are subscribers
        }
    }
    public class HabitBreakGoal : Goal // Specific Goal Type that inherits from Goal
    {
        public int DaysSinceHabitRelapse { get; set; }
        public Action<HabitBreakGoal> OnRelapse; // Action delegate to handle relapse events
        public HabitBreakGoal(string name, string howOften, string frequency) 
            : base(name, howOften, frequency)
        {
            DaysSinceHabitRelapse = 0;
        }
        public void IncrementDaysSinceRelapse() //Method to increment the DaysSinceHabitRelapse property
        {
            DaysSinceHabitRelapse++;
        }
        public void Relapse() //Method to reset the DaysSinceHabitRelapse property to 0
        {
            DaysSinceHabitRelapse = 0;
            OnRelapse?.Invoke(this); // Invoke the OnRelapse action if there are subscribers)
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
        public StreakMilestoneHandler OnMilestone;   // fires when a milestone is hit
        private readonly int[] milestones = { 7, 30, 100 };
        public HabitMakeGoal(string name, string howOften, string frequency) 
            : base(name, howOften, frequency)
        {
            HabitStreak = 0;
        }
        public void IncrementStreak() //Method to increment the HabitStreak property
        {
            HabitStreak++;
            if (milestones.Contains(HabitStreak))
            {
                OnMilestone?.Invoke(this, HabitStreak);
            }
        }
        public void BreakStreak()
        {
            HabitStreak = 0;
        }
        public override void DisplayGoal()
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
            var goal = new Goal("Test Goal", "Daily", "Weekly");
            goal.OnCompleted += (goal) => Console.WriteLine("Goal completed!"); //Lambda expression to handle the OnCompleted event, and multicasting to showcase delegates
            goal.MarkAsCompleted();
        }
    }
}
