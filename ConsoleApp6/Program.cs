using ConsoleApp6.Data;
using ConsoleApp6.Models;

Console.WriteLine("==============================================");
Console.WriteLine("   ReadMore Books - Database Initialization  ");
Console.WriteLine("==============================================\n");

using var context = new BookstoreDbContext();

// Ensure the database is created (applies schema based on the model)
bool created = context.Database.EnsureCreated();

if (created)
{
    Console.WriteLine("✔ Database created successfully for the first time.\n");

    // ── Seed Categories ──────────────────────────────────────────────
    var fiction = new Category
    {
        Name = "Fiction",
        Description = "Novels and stories from the imagination.",
        IsActive = true
    };
    var nonFiction = new Category
    {
        Name = "Non-Fiction",
        Description = "Factual books grounded in real events and information.",
        IsActive = true
    };
    var science = new Category
    {
        Name = "Science",
        Description = "Books covering scientific topics and discoveries.",
        IsActive = true
    };

    context.Categories.AddRange(fiction, nonFiction, science);

    // ── Seed Authors ──────────────────────────────────────────────────
    var tolkien = new Author
    {
        FirstName = "J.R.R.",
        LastName = "Tolkien",
        Email = "tolkien@readmore.com",
        Biography = "English author and philologist, creator of Middle-earth.",
        DateOfBirth = new DateTime(1892, 1, 3)
    };
    var orwell = new Author
    {
        FirstName = "George",
        LastName = "Orwell",
        Email = "orwell@readmore.com",
        Biography = "English novelist and essayist known for dystopian fiction.",
        DateOfBirth = new DateTime(1903, 6, 25)
    };
    var hawking = new Author
    {
        FirstName = "Stephen",
        LastName = "Hawking",
        Email = "hawking@readmore.com",
        Biography = "Theoretical physicist and cosmologist.",
        DateOfBirth = new DateTime(1942, 1, 8)
    };

    context.Authors.AddRange(tolkien, orwell, hawking);

    // ── Seed Books ────────────────────────────────────────────────────
    context.Books.AddRange(
        new Book
        {
            Title = "The Lord of the Rings",
            ISBN = "978-0-618-64015-7",
            Price = 29.99m,
            Pages = 1178,
            PublishedYear = 1954,
            IsInStock = true,
            Author = tolkien,
            Category = fiction
        },
        new Book
        {
            Title = "Nineteen Eighty-Four",
            ISBN = "978-0-452-28423-4",
            Price = 14.99m,
            Pages = 328,
            PublishedYear = 1949,
            IsInStock = true,
            Author = orwell,
            Category = fiction
        },
        new Book
        {
            Title = "A Brief History of Time",
            ISBN = "978-0-553-38016-3",
            Price = 18.50m,
            Pages = 212,
            PublishedYear = 1988,
            IsInStock = false,
            Author = hawking,
            Category = science
        }
    );

    context.SaveChanges();
    Console.WriteLine("✔ Seed data inserted (categories, authors, books).\n");
}
else
{
    Console.WriteLine("ℹ Database already exists — skipping creation.\n");
}

// ── Display all books with related data ──────────────────────────────
Console.WriteLine("── Books in the ReadMore inventory ──────────────");
var books = context.Books
    .OrderBy(b => b.PublishedYear)
    .ToList();

foreach (var book in books)
{
    // Load related entities explicitly (simple approach without lazy loading)
    context.Entry(book).Reference(b => b.Author).Load();
    context.Entry(book).Reference(b => b.Category).Load();

    Console.WriteLine($"\n📖 {book.Title}");
    Console.WriteLine($"   ISBN       : {book.ISBN}");
    Console.WriteLine($"   Author     : {book.Author.FirstName} {book.Author.LastName}");
    Console.WriteLine($"   Category   : {book.Category.Name}");
    Console.WriteLine($"   Price      : ${book.Price:F2}");
    Console.WriteLine($"   Pages      : {book.Pages}");
    Console.WriteLine($"   Published  : {book.PublishedYear}");
    Console.WriteLine($"   In Stock   : {(book.IsInStock ? "Yes" : "No")}");
}

Console.WriteLine("\n==============================================");
Console.WriteLine("   Database demonstration complete!");
Console.WriteLine("==============================================");
