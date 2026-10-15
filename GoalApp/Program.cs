using GoalApp;
namespace GoalApp
{
    public class Program //Claude AI used to generate temporary code structure and formatting for Program class for demonstration purposes
    {
        public static void Main(string[] args)
        {
            // 1. Abstract + sealed: "new Goal(...)" no longer compiles, so create subclasses
            Section("1. Creating goals");
            var read = new HabitMakeGoal("Read 20 minutes", "Daily", "Evening");
            var quit = new HabitBreakGoal("Quit smoking", "Daily", "Morning");
            var gym = new ExerciseGoal("Leg day", "Weekly", "Monday morning");
            Console.WriteLine("Created: HabitMakeGoal, HabitBreakGoal, ExerciseGoal");

            // Delegates: multicast event, custom delegate, Action<T>, lambdas
            read.OnCompleted += c => Console.WriteLine($"  [OnCompleted #1] {c.Name} is done!"); //LAMBDA expression as event handler
            read.OnCompleted += c => Console.WriteLine($"  [OnCompleted #2] Second subscriber notified about {c.Name}");
            read.OnMilestone += (habit, streak) => Console.WriteLine($"  [OnMilestone] {habit.Name} reached a {streak}-day streak!");
            quit.OnRelapse += b => Console.WriteLine($"  [OnRelapse] {b.Name}: counter reset to {b.DaysSinceHabitRelapse}");

            // 2. Method overloading: the four AddExercise versions
            Section("2. Method overloading (AddExercise)");
            gym.AddExercise("Squats", 12);                              // Overload 1
            gym.AddExercise("Plank");                                   // Overload 2
            gym.AddExercise(10, "Lunges", "Calf raises");               // Overload 3 (params)
            gym.AddExercise(new List<(string ExerciseType, int Reps)>   // Overload 4 (batch)
            {
                ("Deadlifts", 8),
                ("Leg press", 15)
            });
            Console.WriteLine($"Exercises added: {gym.Exercises.Count}");

            // 3. Overriding + polymorphism: list typed as Goal, each object runs its own override
            List<Goal> goals = new() { read, quit, gym };

            Section("3. Overriding / polymorphism (DisplayGoal)");
            foreach (var goal in goals)
            {
                goal.DisplayGoal();
                Console.WriteLine();
            }
            // 4. Abstract method: each subclass provides its own implementation of GetProgressSummary
            Section("4. Abstract method (GetProgressSummary)");
            foreach (var goal in goals)
            {
                Console.WriteLine(goal.GetProgressSummary());
            }

            // 5. Interface ITrackable: ExerciseGoal isn't one, so OfType filters it out
            Section("5. Interface (ITrackable): recording 7 days of progress");
            List<ITrackable> trackables = goals.OfType<ITrackable>().ToList();
            for (int day = 1; day <= 7; day++)
            {
                foreach (var item in trackables)
                {
                    item.RecordProgress();   // milestone fires at day 7 for the reading goal
                }
            }
            foreach (var item in trackables)
            {
                var name = item is Goal owner ? owner.Name : "Unknown";
                Console.WriteLine($"{name}: CurrentCount = {item.CurrentCount}");
            }

            // 6. Interface IResettable: Reset() means different things in each class
            Section("6. Interface (IResettable): resetting goals");
            foreach (var resettable in goals.OfType<IResettable>())
            {
                resettable.Reset();
            }
            foreach (var item in trackables)
            {
                var name = item is Goal owner ? owner.Name : "Unknown";
                Console.WriteLine($"{name}: CurrentCount = {item.CurrentCount}");
            }

            // 7. Completion event with two subscribers
            Section("7. Completion event (multicast)");
            read.MarkAsCompleted();

            // 8. Lambdas with LINQ
            Section("8. LINQ with lambdas");

            var pending = goals.Where(g => !g.IsCompleted).Select(g => g.Name);
            Console.WriteLine($"Not completed: {string.Join(", ", pending)}");

            bool anyDone = goals.Any(g => g.IsCompleted);
            Console.WriteLine($"Any goal completed? {anyDone}");

            int totalReps = goals.OfType<ExerciseGoal>()
                                 .SelectMany(e => e.Exercises)
                                 .Sum(x => x.Reps);
            Console.WriteLine($"Total reps across all exercise goals: {totalReps}");

            var ranked = gym.Exercises
                            .OrderByDescending(x => x.Reps)
                            .Select(x => $"{x.ExerciseType} ({x.Reps})");
            Console.WriteLine($"Exercises by reps: {string.Join(", ", ranked)}");

            foreach (var group in goals.GroupBy(g => g.GetType().Name))
            {
                Console.WriteLine($"{group.Key}: {group.Count()}");
            }


        }

        private static void Section(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"=== {title} ===");
        }
    }
}


