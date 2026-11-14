using ConsoleApp6.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp6.Data;

public class BookstoreDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=ReadMoreBooksDb;Trusted_Connection=True;");
    }
}
