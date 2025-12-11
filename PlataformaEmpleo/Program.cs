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
    options.Password.RequireDigit = false; // Requiere al menos un dígito numérico
    options.Password.RequireLowercase = true; // requiere al menos una letra minúscula
    options.Password.RequireUppercase = true; // requiere al menos una letra mayúscula
    options.Password.RequireNonAlphanumeric = true; // requiere caracteres alfanuméricos
    options.Password.RequiredLength = 6; // longitud mInima de la contraseña

}).AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages(); // para las páginas Razor

var app = builder.Build();
app.MapRazorPages(); // Mapeo de las páginas Razor

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(); // Habilitar el uso de archivos estáticos
app.UseRouting();

app.UseAuthentication(); //para la autenticaciòn de usuarios
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
