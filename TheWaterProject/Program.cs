using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWaterProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IntexDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IntexConnection"]);
});

builder.Services.AddScoped<IIntexRepository, EFIntexRepository>();

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("pagenumandtype", "{productCategory}/Page{pageNum}", new { Controller = "Product", Action = "Index" });
app.MapControllerRoute("products", "Products/{pageNum}", new { Controller = "Product", Action = "Index", pageNum = 1 });
app.MapControllerRoute("productCategory", "{productCategory}", new { Controller = "Product", Action = "Index", pageNum = 1 });
app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Product", Action = "Index", pageNum = 1 });
app.MapControllerRoute("productDetails", "ProductDetails/{productId}", new { Controller = "ProductDetails", Action = "ProductDetails" });
app.MapControllerRoute("OrderReview","OrderReview/{orderId?}",new { Controller = "OrderReview", Action = "OrderReview" });
//CRUD PRODUCT ROUTES BELOW
app.MapControllerRoute("ManageProducts", "Admin/ManageProducts", new { Controller = "Admin", Action = "ManageProducts" });
app.MapControllerRoute("CreateProduct", "Admin/CreateProduct", new { Controller = "Admin", Action = "CreateProduct" });
// CRUD USER ROUTES BELOW
// (UNCOMMENT WHEN THE CONTROLLER IS FIXED)
// app.MapControllerRoute("ManageUsers", "Admin/ManageUsers", new { Controller = "Admin", Action = "ManageUsers" });
// app.MapControllerRoute("CreateUser", "Admin/CreateUsers", new { Controller = "Admin", Action = "CreateUser" });

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();