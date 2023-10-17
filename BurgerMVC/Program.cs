using BurgerMVC.Areas.Admin.AdminRepository;
using BurgerMVC.Areas.Admin.AdminRepository.AdminInterfaces;
using BurgerMVC.Areas.Services;
using BurgerMVC.Context;
using BurgerMVC.Dbinitializer;
using BurgerMVC.Models;
using BurgerMVC.Repository;
using BurgerMVC.Repository.Interfaces;
using FastReport.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPaging(options =>
       {
           options.ViewName = "Bootstrap5";
           options.PageParameterName = "pageindex";

       });
builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<RelatorioLanchesService>();
builder.Services.AddScoped<GraficoVendasService>();
builder.Services.AddScoped<ILancheRepository, LancheRepository>();
builder.Services.AddScoped<IAdminLancheRepository, AdminLancheRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ConfigurationImages>(builder.Configuration.GetSection("ConfigurationPastaImagens"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
    AddEntityFrameworkStores<AppDbContext>().
    AddDefaultTokenProviders();
builder.Services.AddAuthorization(options =>
options.AddPolicy("Admin", policy => policy.RequireRole("Admin")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped(x => CarrinhoCompra.GetCarrinhoCompra(x));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddFastReport();
FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    // use dbInitializer
    dbInitializer.Seed();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseFastReport();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "CategoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { Controller = "Lanche", Action = "List" }
     );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
