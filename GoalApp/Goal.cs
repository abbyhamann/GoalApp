using System;
using System.Collections.Generic;
using System.Linq;

namespace GoalApp
{
    public delegate void GoalCompletedEventHandler(Goal goal); // DELEGATE for goal completion event
    public delegate void StreakMilestoneHandler(HabitMakeGoal goal, int streak); //Claude AI suggested making a delegate for the HabitMakeGoal
                                                                                 //and HabitBreakGoal class to handle streak milestones
    public interface ITrackable // INTERFACE for goals that can track progress
                                // It defines a property for the current count and a method to record progress.
                                //Both are interfaces because classes can implement multiple interfaces,
                                //as opposed to inheriting from a single base class.
    {
        int CurrentCount { get; }
        void RecordProgress();
    }

    public interface IResettable // INTERFACE for goals that can be reset
                                 // It defines a method to reset the goal's progress.
    {
        void Reset();
    }
    public abstract class Goal // Base class for goals. ABSTRACT because it's a template and should not be instantiated directly.
                               // It provides common properties and methods for all goal types.
                               // Claude AI suggested making the existing class abstract.
    {
        public string Name { get; set; }
        public string HowOften { get; set; }
        public string ReminderFrequency { get; set; }
        public bool IsCompleted { get; private set; }
        public event GoalCompletedEventHandler? OnCompleted; // Delegate event to notify when a goal is completed 
                                                             //Events are a specific kind of delegate that are used to provide notifications.
                                                             //They are typically used in scenarios where a class needs to notify other classes
                                                             //or components when something of interest occurs.


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
            if (IsCompleted) return;
            IsCompleted = true;
            OnCompleted?.Invoke(this); // Invoke the OnCompleted event if there are subscribers
        }
        public abstract string GetProgressSummary(); //abstract function to be implemented by subclasses
                                                     //to provide a summary of the goal's progress
    }

    public sealed class HabitMakeGoal : Goal, ITrackable, IResettable //Inherits from Goal and IMPLEMENTS ITrackable and IResettable interfaces 
                                                                      // This and the other two subclasses are SEALED because they are the end of the inheritance chain.
                                                                      //This is a design choice to prevent further subclassing and maintain the integrity of the goal types.
    {
        public int HabitStreak { get; private set; } //By making it private, we ensure that the streak can only be modified
                                                     //through the IncrementStreak and BreakStreak methods,
                                                     //which can include additional logic (like milestone checks).
        public int CurrentCount => HabitStreak; // Implementing ITrackable interface property
        public void RecordProgress() => IncrementStreak(); // Implementing ITrackable interface method
        public void Reset() => BreakStreak(); // Implementing IResettable interface method
        public event StreakMilestoneHandler? OnMilestone;   // fires when a milestone is hit
        private static readonly int[] milestones = { 7, 30, 100 };
        public HabitMakeGoal(string name, string howOften, string frequency)
            : base(name, howOften, frequency)
        {
            HabitStreak = 0;
        }
        public override string GetProgressSummary() => $"{Name}: {HabitStreak}-day streak"; //OVERRIDE abstract method to provide a summary of the goal's progress
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
        public override void DisplayGoal() //OVERRIDES the DisplayGoal method to include HabitStreak
        {
            base.DisplayGoal();
            Console.WriteLine($"Habit Streak: {HabitStreak}");
        }
    }
    public sealed class HabitBreakGoal : Goal, ITrackable, IResettable // Specific Goal Type that inherits from Goal
    {
        public int DaysSinceHabitRelapse { get; private set; }
        public int CurrentCount => DaysSinceHabitRelapse; // Implementing ITrackable interface property
        public void RecordProgress() => IncrementDaysSinceRelapse(); // Implementing ITrackable interface method
        public void Reset() => Relapse(); // Implementing IResettable interface method
        public event Action<HabitBreakGoal>? OnRelapse; // Event to handle relapse events
        public HabitBreakGoal(string name, string howOften, string frequency)
            : base(name, howOften, frequency)
        {
            DaysSinceHabitRelapse = 0;
        }
        public override string GetProgressSummary() => $"{Name}: {DaysSinceHabitRelapse} days clean";
        //override abstract method to provide a summary of the goal's progress
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
    public sealed class ExerciseGoal : Goal // Inherits from Goal
    {
        public List<(string ExerciseType, int Reps)> Exercises { get; } = new(); // List to hold exercises and their reps

        public ExerciseGoal(string name, string howOften, string frequency)
            : base(name, howOften, frequency)
        {
            Exercises = new List<(string, int)>();
        }
        public override string GetProgressSummary() => $"{Name}: {Exercises.Count} exercises planned";
        // Overload 1: add a single exercise with reps
        //used Claude AI to help with formatting and code structure.
        public void AddExercise(string exerciseType, int reps)
        {
            if (string.IsNullOrWhiteSpace(exerciseType))
                throw new ArgumentException("Exercise name required.", nameof(exerciseType));
            if (reps < 0)
                throw new ArgumentOutOfRangeException(nameof(reps));
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

}

