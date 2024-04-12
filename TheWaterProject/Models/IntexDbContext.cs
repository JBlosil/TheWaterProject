using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TheWaterProject.Models;

public class IntexDbContext : IdentityDbContext
{
    public IntexDbContext(DbContextOptions<IntexDbContext> options)
        : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<LineItem> LineItems { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }
    
    public DbSet<TopProducts> TopProducts { get; set; }
    public DbSet<UserTopProducts> UserTopProducts { get; set; }
    public DbSet<ItemRecommendations> ItemRecommendations { get; set; }
    
}
