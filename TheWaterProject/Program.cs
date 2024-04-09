using Microsoft.EntityFrameworkCore;
using TheWaterProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WaterProjectContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:WaterConnection"]);
});

builder.Services.AddScoped<IWaterRepository, EFWaterRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute("pagenumandtype", "{projectType}/Page{pageNum}", new { Controller = "Home", Action = "Index" });
app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("projectType", "{projectType}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("pagination", "Projects/Page{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });


app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();