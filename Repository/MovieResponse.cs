using FilmesAPI.Models;
using Newtonsoft.Json.Linq;

namespace FilmesAPI.Repository;

public class MovieResponse
{
    private static IEnumerable<Movie> movieList = GetAllMovies();
    public static IEnumerable<Movie> GetMovies()
    {
        return movieList;
    }

    public static Movie? GetMovieById(int id)
    {
        return movieList.FirstOrDefault((m => m.Id == id), null);
    }

    public static IEnumerable<Movie> GetMoviesByGenre(string genreParam)
    {
        var movieListByGenre = new List<Movie>();
        foreach (var movie in movieList)
        {
            foreach (var genre in movie.Genres)
            {
                if (genre.Name == genreParam)
                    movieListByGenre.Add(movie);
            }
        }

        return movieListByGenre;
    }

    private static IEnumerable<Movie> GetAllMovies()
    {
        var path = "Repository/movies_data.json";
        var listaDeFilmes = new List<Movie>();
        var GenresList = GenreResponse.GetAllGenres();

        JObject readJson;

        StreamReader leitor;

        using (var moviesJson = new FileStream(path, FileMode.Open))
        {
            leitor = new StreamReader(moviesJson);
            readJson = JObject.Parse(leitor.ReadToEnd());
        }

        if (readJson["results"] != null)
        {
            IEnumerable<JToken> result = readJson["results"].Children().ToList();

            foreach (var movie in result)
            {
                Movie movieResultItem = movie.ToObject<Movie>();
                var jsonGenreIds = (JArray)movie["genre_ids"];
                var listGenreIds = jsonGenreIds.ToObject<List<int>>();
                var listGenres = new List<Genre>();
                foreach (var genreId in listGenreIds)
                {
                    var genre = GenresList.First(g => g.Id == genreId);
                    if (genre != null)
                    {
                        movieResultItem.Genres.Add(genre);
                    }
                }
                movieResultItem.ReleaseDate = (string)movie["release_date"];
                movieResultItem.VoteAverage = (int)movie["vote_average"] / 2;
                listaDeFilmes.Add(movieResultItem);
            }
        }

        return listaDeFilmes;
    }

    public static IEnumerable<Movie> GetMoviesByYear(int year)
    {
        var movies = GetAllMovies();
        var moviesByYear = new List<Movie>();
        foreach (var movie in movies)
        {
            if (movie.ReleaseDate.Split("-").FirstOrDefault() == year.ToString())
            {
                moviesByYear.Add(movie);
            }
        };

        return moviesByYear;
    }
}
