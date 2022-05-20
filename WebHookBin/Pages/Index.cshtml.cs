using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebHookBin.Database;
using WebHookBin.Database.Model;

namespace WebHookBin.Pages {
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly LogDatabaseContext databaseContext;

        public ICollection<LogEntry> Requests { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, LogDatabaseContext databaseContext)
        {
            this._logger = logger;
            this.databaseContext = databaseContext;
        }

        public void OnGet()
        {
            this.Requests = this.databaseContext.LogEntries
                .Include(le => le.Headers)
                .Include(le => le.QueryParameters)
                .OrderByDescending(le => le.Timestamp)
                .ToList();
        }

        public object? PrettyPrint(LogEntry request)
        {
            try
            {
                return JsonConvert.DeserializeObject(request.RawBody);
            }
            catch
            {
                return request.RawBody;
            }
        }
    }
}
