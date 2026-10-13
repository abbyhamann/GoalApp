/// Created using AI in order to allow me to prioratise the C# 
/// aspect of the project and not the database aspects of the project


using Microsoft.EntityFrameworkCore;

namespace GoalApp.Data
{
    public class GoalContext : DbContext
    {
        public DbSet<GoalRecord> Goals { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data Source=goals.db");
            }
        }
    }
}
