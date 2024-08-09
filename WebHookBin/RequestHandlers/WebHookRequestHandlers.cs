using WebHookBin.Database;
using WebHookBin.Database.Model;
using HttpMethod = WebHookBin.Database.Model.HttpMethod;

namespace WebHookBin.RequestHandlers {
    public static class WebHookRequestHandlers
    {
        public static async Task HookHandler(HttpContext context)
        {

            var dbContext = context.RequestServices.GetRequiredService<LogDatabaseContext>();

            using var streamReader = new StreamReader(context.Request.Body);

            dbContext.LogEntries.Add(
                new LogEntry(
                    Timestamp: DateTime.Now,
                    Method: GetHttpMethod(context.Request.Method),
                    Path: context.Request.Path,
                    RawBody: await streamReader.ReadToEndAsync()
                )
                {
                    Headers = context.Request.Headers.SelectMany(
                        header => header.Value.Select(
                            headerValue => new Header(
                                Key: header.Key,
                                Value: headerValue ?? ""
                            )
                        )
                    ).ToList(),
                    QueryParameters = context.Request.Query.SelectMany(
                        queryParameter => queryParameter.Value.Select(
                            queryParameterValue => new QueryParameter(
                                Key: queryParameter.Key,
                                Value: queryParameterValue
                            )
                        )
                    ).ToList(),
                }
            );

            await dbContext.SaveChangesAsync();

            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("");
        }

        private static HttpMethod GetHttpMethod(string method)
            => Enum.GetValues<HttpMethod>()
                .Cast<HttpMethod?>()
                .FirstOrDefault(x => Enum.GetName(x!.Value)!.Equals(method, StringComparison.InvariantCultureIgnoreCase))
            ?? HttpMethod.Unknown;
    }
}
