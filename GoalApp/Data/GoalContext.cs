/// Created using AI in order to allow me to prioratise the C# 
/// aspect of the project and not the database aspects of the project

using Microsoft.EntityFrameworkCore;

public class GoalContext : DbContext
{
    public DbSet<Goal> Goals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=goals.db");
    }
}