using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace GoalApp.Data
{
    // Represents one goal saved in the SQLite database.
    // This is separate from the application's abstract Goal class.
    public class GoalRecord
    {
        // EF Core recognizes GoalId as the primary key by convention.
        [Key]
        public int GoalId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string User_ID { get; set; } = string.Empty;

        public string HowOften { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        // Stored as an integer, even if an older method used strings.
        public int Streak { get; set; }

        public string? Description { get; set; }

        public string GoalType { get; set; } = string.Empty;

        public string? Milestones { get; set; }

        public DateTime? DueDate { get; set; }

        // Used to store the reminder information from the Goal hierarchy.
        public string? ReminderFrequency { get; set; }
    }
}
