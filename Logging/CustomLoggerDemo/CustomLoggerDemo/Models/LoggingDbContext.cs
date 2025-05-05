using Microsoft.EntityFrameworkCore;

namespace CustomLoggerDemo.Models
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options): base(options) 
        {

        }

        public DbSet<LogEntry> LogEntries { get; set; } 
    }
}
