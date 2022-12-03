using Backend.WebAPI.DataAccess;
using Backend.MVC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IThingService, ThingService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ThingsContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThingsContextConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
