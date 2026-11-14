namespace ConsoleApp6.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public DateTime DateOfBirth { get; set; }

    // Navigation property
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
