using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persitense;

public class DataContext:IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<ReadBook> ReadBooks { get; set; }
}