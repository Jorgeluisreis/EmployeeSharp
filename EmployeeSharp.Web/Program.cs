using EmployeeSharp.Infra.Data;
using EmployeeSharp.Web.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.AddEmployeeSharpServices();

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

using var serviceScope = app.Services.CreateScope();

var context = serviceScope.ServiceProvider.GetRequiredService<EmployeeSharpContext>();

await context.Database.MigrateAsync();

serviceScope.Dispose();

await app.RunAsync();