using Microsoft.EntityFrameworkCore;

namespace Registration_Service;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<Player> Players { get; set; }
}