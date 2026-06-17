using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Infrastructure.Data;
using LeveInvestimentos.Core.Interfaces;
using LeveInvestimentos.Infrastructure.Repositories;
using LeveInvestimentos.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "LeveInvestimentosAuth";
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Home/AcessoNegado";
    });

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllersWithViews();

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (app.Environment.IsDevelopment())
    {
        context.Database.EnsureCreated(); 
        DataSeeder.Seed(context);
    }
}

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();