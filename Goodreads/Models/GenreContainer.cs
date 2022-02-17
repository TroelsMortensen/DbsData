namespace Goodreads.Models;

public class GenreContainer
{
    public int Id { get; set; }
    public string Genre { get; set; }

    public override bool Equals(object? obj)
    {
        GenreContainer gc = (GenreContainer)obj;
        return Id == gc.Id && Genre.Equals(gc.Genre);
    }
}