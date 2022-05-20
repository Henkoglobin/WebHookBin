using Microsoft.EntityFrameworkCore;

namespace WebHookBin.Database {
    public class SqliteLogDatabaseContext : LogDatabaseContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionStringName;

        public SqliteLogDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionStringName = configuration["Database:ConnectionStringName"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(
                this.configuration.GetConnectionString(this.connectionStringName)
            );
        }
    }
}
