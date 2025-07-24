using GadgetHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Data;

public class GadgetHubContext : DbContext
{
    public GadgetHubContext(DbContextOptions<GadgetHubContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Quotation> Quotations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
}