using Microsoft.EntityFrameworkCore;

namespace BeerDotNetCore.Models;

public class BeerContext : DbContext
{
    public BeerContext(DbContextOptions<BeerContext> options)
        : base(options)
    {
    }

    public DbSet<Beer> Beers { get; set; } = null!;
}