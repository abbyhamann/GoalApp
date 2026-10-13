/*using System;


public class Read_Writter_Manager
{
    public Read_Writter_Manager()
    {
        /// Initiate the Read_Writter class
        Read_Writter WR = new Read_Writter();
    }

    public void create_goal(string Name, string User_ID, string HowOften, bool IsCompleted, string Streak,
            string Description, string GoalType, string Milestones)
    {
        /// Fill generic string that stands in until I create a database to connect to. 
        /// This will be used to connect to the database and save the goal.
        string connectionString = "Server=localhost;Database=GoalDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

        GoalDatabase database = new GoalDatabase(connectionString);

        Goal goal = new Goal 
        {
            Name = Name,
            User_ID = User_ID,
            HowOften = HowOften,
            IsCompleted = IsCompleted,
            Streak = Streak,
            Description = Description,
            GoalType = GoalType,
            Milestones = Milestones
        };

        int goalId = database.AddGoal(goal);

        Console.WriteLine($"Goal saved with ID: {goalId}");
    }


    public void read_goal()
    {
        using (var db = new GoalContext())
        {
            List<Goal> goals = db.Goals.ToList();
            foreach (Goal goal in goals)
            {
                Console.WriteLine($"ID: {goal.GoalId}, Name: {goal.Name}, User ID: {goal.User_ID}, How Often: {goal.HowOften}, Is Completed: {goal.IsCompleted}, Streak: {goal.Streak}, Description: {goal.Description}, Goal Type: {goal.GoalType}, Milestones: {goal.Milestones}");
            }
        }
    }

    public void edit_goal()
    {
        using (var db = new GoalContext())
        {
            List<Goal> goals = db.Goals.ToList();

            foreach (Goal goal in goals)
            {
                Console.WriteLine(goal.Name);
            }

            Console.WriteLine("Enter the ID of the goal you want to edit:");
            string goal_user_wants_to_edit = Console.ReadLine();

            Goal goal_to_edit = db.Goals.FirstOrDefault(g => g.GoalId.ToString() == goal_user_wants_to_edit);

            Console.WriteLine("What do you want to edit? " +
                "(Everything, Name, User_ID, HowOften, " +
                "IsCompleted, Streak, Description, GoalType, Milestones)");

            string user_edit_choice = Console.ReadLine();

            if (user_edit_choice == "Everything")
            {
                Console.WriteLine("Enter new Name:");
                goal_to_edit.Name = Console.ReadLine();
                Console.WriteLine("Enter new User_ID:");
                goal_to_edit.User_ID = Console.ReadLine();
                Console.WriteLine("Enter new HowOften:");
                goal_to_edit.HowOften = Console.ReadLine();
                Console.WriteLine("Is the goal completed? (true/false):");
                goal_to_edit.IsCompleted = bool.Parse(Console.ReadLine());
                Console.WriteLine("Enter new Streak:");
                goal_to_edit.Streak = Console.ReadLine();
                Console.WriteLine("Enter new Description:");
                goal_to_edit.Description = Console.ReadLine();
                Console.WriteLine("Enter new GoalType:");
                goal_to_edit.GoalType = Console.ReadLine();
                Console.WriteLine("Enter new Milestones:");
                goal_to_edit.Milestones = Console.ReadLine();
            }
            else if (user_edit_choice == "Name")
            {
                Console.WriteLine("Enter new Name:");
                goal_to_edit.Name = Console.ReadLine();
            }
            else if (user_edit_choice == "User_ID")
            {
                Console.WriteLine("Enter new User_ID:");
                goal_to_edit.User_ID = Console.ReadLine();
            }
            else if (user_edit_choice == "HowOften")
            {
                Console.WriteLine("Enter new HowOften:");
                goal_to_edit.HowOften = Console.ReadLine();
            }
            else if (user_edit_choice == "IsCompleted")
            {
                Console.WriteLine("Is the goal completed? (true/false):");
                goal_to_edit.IsCompleted = bool.Parse(Console.ReadLine());
            }
            else if (user_edit_choice == "Streak")
            {
                Console.WriteLine("Enter new Streak:");
                goal_to_edit.Streak = Console.ReadLine();
            }
            else if (user_edit_choice == "Description")
            {
                Console.WriteLine("Enter new Description:");
                goal_to_edit.Description = Console.ReadLine();
            }
            else if (user_edit_choice == "GoalType")
            {
                Console.WriteLine("Enter new GoalType:");
                goal_to_edit.GoalType = Console.ReadLine();
            }
            else if (user_edit_choice == "Milestones")
            {
                Console.WriteLine("Enter new Milestones:");
                goal_to_edit.Milestones = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }

    public void delete_goal()
    {

    }
}*/


using System;
using GoalApp.Data;

public class Read_Writter_Manager
{
    private readonly Read_Writter writer = new Read_Writter();

    // Create and save a database record.
    public void create_goal(
        string Name,
        string User_ID,
        string HowOften,
        bool IsCompleted,
        string Streak,
        string Description,
        string GoalType,
        string Milestones)
    {
        if (!int.TryParse(Streak, out int streakValue))
        {
            throw new ArgumentException(
                "Streak must be a whole number.", nameof(Streak));
        }

        var goal = new GoalRecord
        {
            Name = Name,
            User_ID = User_ID,
            HowOften = HowOften,
            IsCompleted = IsCompleted,
            Streak = streakValue,
            Description = Description,
            GoalType = GoalType,
            Milestones = Milestones
        };

        int goalId = writer.AddGoal(goal);

        Console.WriteLine($"Goal saved with ID: {goalId}");
    }

    // Read and display all saved goals.
    public void read_goal()
    {
        var goals = writer.GetGoals();

        foreach (var goal in goals)
        {
            Console.WriteLine(
                $"ID: {goal.GoalId}, Name: {goal.Name}, " +
                $"User: {goal.User_ID}, Type: {goal.GoalType}, " +
                $"Completed: {goal.IsCompleted}, Streak: {goal.Streak}");
        }
    }

    // Edit a goal's name.
    public void edit_goal()
    {
        read_goal();

        Console.WriteLine("Enter the ID of the goal to edit:");
        if (!int.TryParse(Console.ReadLine(), out int goalId))
        {
            Console.WriteLine("Invalid goal ID.");
            return;
        }

        var goal = writer.GetGoals()
            .Find(g => g.GoalId == goalId);

        if (goal == null)
        {
            Console.WriteLine("Goal not found.");
            return;
        }

        Console.WriteLine("Enter the new name:");
        string? newName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        goal.Name = newName;

        if (writer.EditGoal(goal))
        {
            Console.WriteLine("Goal updated.");
        }
    }

    // Delete a goal by ID.
    public void delete_goal()
    {
        read_goal();

        Console.WriteLine("Enter the ID of the goal to delete:");
        if (!int.TryParse(Console.ReadLine(), out int goalId))
        {
            Console.WriteLine("Invalid goal ID.");
            return;
        }

        if (writer.DeleteGoal(goalId))
        {
            Console.WriteLine("Goal deleted.");
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }
}


