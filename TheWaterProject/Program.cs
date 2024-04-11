using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWaterProject.Models;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IntexDbContext>();

builder.Services.AddScoped<IIntexRepository, EFIntexRepository>();

builder.Services.AddRazorPages();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
});

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
app.UseCookiePolicy();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("pagenumandtype", "{productCategory}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
app.MapControllerRoute("products", "Products/{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });
app.MapControllerRoute("productCategory", "{productCategory}", new { Controller = "Home", Action = "Products", pageNum = 1 });
app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });
app.MapControllerRoute("productDetails", "ProductDetails/{productId}", new { Controller = "Home", Action = "ProductDetails" });
app.MapControllerRoute("OrderReview","OrderReview/{orderId?}",new { Controller = "Home", Action = "OrderReview" });
//CRUD PRODUCT ROUTES BELOW
app.MapControllerRoute("ManageProducts", "Admin/ManageProducts", new { Controller = "Home", Action = "ManageProducts" });
app.MapControllerRoute("CreateProduct", "Admin/CreateProduct", new { Controller = "Home", Action = "CreateProduct" });
// CRUD USER ROUTES BELOW
// (UNCOMMENT WHEN THE CONTROLLER IS FIXED)
// app.MapControllerRoute("ManageUsers", "Admin/ManageUsers", new { Controller = "Admin", Action = "ManageUsers" });
// app.MapControllerRoute("CreateUser", "Admin/CreateUsers", new { Controller = "Admin", Action = "CreateUser" });

app.MapDefaultControllerRoute();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Customer" };

    foreach (var role in roles)
    {
        
        if (! await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));
        
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@admin.com";
    string password = "Admin1234!";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();