using GoalApp.Data;
using System;
using System.Collections.Generic;
using System.Text;





using GoalApp.Data;

namespace GoalApp
{
    internal class tempWorkspace
    {
        public static void Main(string[] args)
        {
            // Create an object representing the goal to save.
            GoalRecord new_goal = new GoalRecord
            {
                Name = "Finish C# Project",
                User_ID = "User123",
                HowOften = "Daily",
                IsCompleted = false,
                Streak = 0,
                Description = "Finish the database portion.",
                GoalType = "School",
                Milestones = "Finish database; Test database"
            };

            // Create the database helper.
            Read_Writter database = new Read_Writter();

            // Call the function to save the object.
            int goalId = database.AddGoal(new_goal);

            Console.WriteLine($"Goal saved with ID: {goalId}");


            // Retrieve the goal with ID 1.
            int goalID = 1;
            GoalRecord? current_goal = database.ReadGoal(goalID);

            if (current_goal != null)
            {
                // Change the object's property.
                current_goal.Name = "Finish Database Project";

                // Save the changes to SQLite.
                bool success = database.EditGoal(current_goal);

                if (success)
                {
                    Console.WriteLine("Goal updated successfully!");
                }
                else
                {
                    Console.WriteLine("Goal could not be updated.");
                }
            }
            else
            {
                Console.WriteLine("Goal not found.");
            }
        }
    }
}
