using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using PlataformaEmpleo.Data;
using Microsoft.AspNetCore.Identity;
using PlataformaEmpleo.Models;

QuestPDF.Settings.License = LicenseType.Community; // Configurar la licencia de QuestPDF

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

builder.Services.AddIdentity<Usuario,IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true; // Requiere al menos un d�gito num�rico
    options.Password.RequireLowercase = true; // requiere al menos una letra min�scula
    options.Password.RequireUppercase = true; // requiere al menos una letra may�scula
    options.Password.RequireNonAlphanumeric = true; // requiere caracteres alfanum�ricos
    options.Password.RequiredLength = 6; // longitud minima de la contrase�a

}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.LoginPath = "/Identity/Account/Login";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages(); // para las p�ginas Razor

var app = builder.Build();
app.MapRazorPages(); // Mapeo de las p�ginas Razor


//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Habilitar el uso de archivos est�ticos
app.UseRouting();

app.UseAuthentication(); //para la autenticacion de usuarios
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
