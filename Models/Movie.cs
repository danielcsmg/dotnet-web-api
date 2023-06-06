namespace FilmesAPI.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Overview { get; set; }
    public string ReleaseDate { get; set; }
    public List<Genre> Genres { get; set; } = new List<Genre>();
    public int VoteAverage { get; set; }
}
