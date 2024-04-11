using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWaterProject.Models;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// key vault
ConfigurationBuilder azureBuilder = new ConfigurationBuilder();
azureBuilder.AddAzureKeyVault(new Uri("https://intex313secrets.vault.azure.net/"), new DefaultAzureCredential());
IConfiguration configuration = azureBuilder.Build();
string connectionString = configuration["ServerConnectionString"];

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["GoogleClientID"];
    googleOptions.ClientSecret = configuration["GoogleClientSecret"];
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IntexDbContext>(options =>
{
    options.UseSqlServer(connectionString); // Direct use of the connection string
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IntexDbContext>();

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

app.MapControllerRoute("pagenumandtype", "{productCategory}/Page{pageNum}", new { Controller = "Product", Action = "Index" });
app.MapControllerRoute("products", "Products/{pageNum}", new { Controller = "Product", Action = "Index", pageNum = 1 });
app.MapControllerRoute("productCategory", "{productCategory}", new { Controller = "Product", Action = "Index", pageNum = 1 });
app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Product", Action = "Index", pageNum = 1 });
app.MapControllerRoute ("productDetails", "ProductDetails/{productId}", new { Controller = "ProductDetails", Action = "ProductDetails" });


app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();