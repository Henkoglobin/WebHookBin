using Microsoft.EntityFrameworkCore;
using WebHookBin;
using WebHookBin.Database;
using WebHookBin.RequestHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddCors();
builder.Services.AddSingleton<AppVersionInfo>();

switch (builder.Configuration["Database:Provider"]) {
    case "Sqlite":
        builder.Services.AddDbContext<LogDatabaseContext, SqliteLogDatabaseContext>();
        break;
    default:
        throw new ArgumentOutOfRangeException("Unknown database provider: " + builder.Configuration["Database:Provider"]);
}

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
}

using (var scope = app.Services.CreateScope()) {
    scope.ServiceProvider
        .GetRequiredService<LogDatabaseContext>()
        .Database.Migrate();
}

app.UseStaticFiles();
app.MapRazorPages();
app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapFallback("/api/hooks/{*path:nonfile}", WebHookRequestHandlers.HookHandler);

app.Run();
