using EmployeeSharp.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var isDevelopment = env.IsDevelopment();

if (isDevelopment)
{
    builder.Logging.AddConsole();
}

builder.Services.AddRazorPages();

var config = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

builder.Services.ConfigureServices(config);
builder.WebHost.ConfigureKestrelServer();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Colaboradores}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();