using System;
using System.Collections.Generic;
using System.Text;
using GoalApp.Data;

namespace GoalApp
{
    public partial class AllGoalPage
    {
        public void DisplayAllGoalsPage()
        {
            // Create the database helper.
            Read_Writter database = new Read_Writter();

            while (true)
            {
                Console.WriteLine("All Goals Page");
                Console.WriteLine("1. View All Goals");
                Console.WriteLine("2. Edit Goal");
                Console.WriteLine("3. Delete Goal");
                Console.WriteLine("4. Add Goal");
                Console.WriteLine("5. Read Goal");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        //ViewAllGoals();
                        break;
                    case "2":
                        //EditGoal();
                        break;
                    case "3":
                        //DeleteGoal();
                        break;

                    case "4":
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

                        // Call the function to save the object.
                        int goalId = database.AddGoal(new_goal);

                        Console.WriteLine($"Goal saved with ID: {goalId}");

                        break;

                    case "5":
                        // Retrieve the goal with ID goalID.
                        Console.Write("Enter goal ID: ");
                        int goalID = int.Parse(Console.ReadLine());
                        GoalRecord? current_goal = database.ReadGoal(goalID);

                        while (true)
                        {
                            Console.WriteLine("Do you want to edit this goal? (y/n)");
                            string editYN = Console.ReadLine();
                            if (editYN.ToLower() == "y")
                            {
                                // Call the EditGoal method to update the goal.
                                if (current_goal != null)
                                {
                                    Console.WriteLine("What do you want to edit? (name/...)?");
                                    string editChoice = Console.ReadLine();

                                    // the options for what can be edited.
                                    if (editChoice.ToLower() == "name")
                                    {
                                        Console.Write("Enter new name: ");
                                        string newName = Console.ReadLine();
                                        current_goal.Name = newName;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid choice. Only 'name' can be edited in this example.");
                                    }

                                    // Confirms the edit and saves the changes to the database.
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
                            else
                            {
                                break;
                            }
                        }
                        
                        break;

                    case "6":
                        return; // Exit the loop and go back to the main menu

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
    }
}

