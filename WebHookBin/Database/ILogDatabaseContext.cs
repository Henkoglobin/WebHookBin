using Microsoft.EntityFrameworkCore;
using WebHookBin.Database.Model;

namespace WebHookBin.Database
{
    public abstract class LogDatabaseContext : DbContext
    {
        public DbSet<LogEntry> LogEntries { get; init; }
    }
}
