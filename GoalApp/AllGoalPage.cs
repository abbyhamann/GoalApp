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
            while (true)
            {
                Console.WriteLine("All Goals Page");
                Console.WriteLine("1. View All Goals");
                Console.WriteLine("2. Edit Goal");
                Console.WriteLine("3. Delete Goal");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewAllGoals();
                        break;
                    case "2":
                        EditGoal();
                        break;
                    case "3":
                        DeleteGoal();
                        break;
                    case "4":
                        return; // Exit the loop and go back to the main menu
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        
    }
}

