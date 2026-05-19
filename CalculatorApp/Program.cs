using System.IO;
using Microsoft.EntityFrameworkCore;
using CalculatorApp.Components;
using CalculatorApp.Data;
using CalculatorApp.Validation;
using CalculatorApp.Services;
using CalculatorApp.Models;
using CalculatorApp.Results;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        var dbPath = Path.Combine(builder.Environment.ContentRootPath, "calculator.db");
        connectionString = $"Data Source={dbPath}";
    }

    options.UseSqlite(connectionString);
});
builder.Services.AddScoped<AuthInputValidator>();
builder.Services.AddScoped<CalculatorInputValidator>();
builder.Services.AddScoped<SessionStateService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CalculatorService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Ensure database migrations are applied on startup so the SQLite file exists.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
