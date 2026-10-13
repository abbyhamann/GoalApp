using System;


public class Read_Writter_Manager
{
    public Read_Writter_Manager()
    {
        /// Initiate the Read_Writter class
        Read_Writter WR = new Read_Writter();
    }

    public create_goal(string Name, string User_ID, string HowOften, bool IsCompleted, string Streak,
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


    public read_goal()
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

    public edit_goal()
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

    public delete_goal()
    {

    }
}

