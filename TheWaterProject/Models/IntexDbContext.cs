using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheWaterProject.Models;

public class IntexDbContext : DbContext
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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LineItem>()
            .HasKey(li => new { li.TransactionId, li.ProductId });
    }
}
