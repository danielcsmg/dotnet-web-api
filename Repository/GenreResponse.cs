using FilmesAPI.Models;
using Newtonsoft.Json.Linq;

namespace FilmesAPI.Repository;

public class GenreResponse
{
    public static IEnumerable<Genre> GetAllGenres()
    {
        var path = "Repository/genres.json";
        var genresList = new List<Genre>();
        JObject readJson;
        StreamReader leitor;
        using (var gendersJson = new FileStream(path, FileMode.Open))
        {
            leitor = new StreamReader(gendersJson);
            readJson = JObject.Parse(leitor.ReadToEnd());
        }

        if (readJson["genres"] != null)
        {
            IEnumerable<JToken> result = readJson["genres"].Children().ToList();

            foreach (var genre in result)
            {
                Genre genreResultItem = genre.ToObject<Genre>();
                genresList.Add(genreResultItem);

            }
        }

        return genresList;
    }
}
