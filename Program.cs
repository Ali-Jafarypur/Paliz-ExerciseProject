using BlazorApp1.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.MSSqlServer;
using System.Configuration;

//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();

// Add Serilog
builder.Services.AddSerilog();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var log = new LoggerConfiguration()
    .Filter.ByExcluding(Matching.FromSource("Microsoft"))
    .Filter.ByExcluding(Matching.FromSource("System"))
    .WriteTo.MSSqlServer(
        connectionString: configuration.GetConnectionString("Default"),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Log", SchemaName = "dbo", AutoCreateSqlTable = true },
        appConfiguration: configuration
        )
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddControllers();
//builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

var app = builder.Build();

Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

//Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
//    .WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
// .WriteTo.File(@"D:\Storage\Work\Paliz\Visual Studio\Projects\Source\repos\BlazorApp1\wwwroot\logs\log.txt")





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapBlazorHub();
//    endpoints.MapFallbackToPage("/_Host");
//});
//app.MapControllerRoute("default", "{controller=Product}/{action=GetProducts}/{id?}");

app.Run();
