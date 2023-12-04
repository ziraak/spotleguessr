using Newtonsoft.Json;

namespace Models
{
    public class NewModel
    {
        public int Rank { get; set; }
        public int Size { get; set; }

        public int DebutAlbum { get; set; }

        public Genre? Genre { get; set; }

        public string Country { get; set; }

        public Gender? Gender { get; set; }
        public string Name { get; set; }

        public static List<NewModel> FromJson(string json) => JsonConvert.DeserializeObject<List<NewModel>>(json);

        public string WriteToConsole()
        {
            return $"name: {Name}, rank:{Rank}, gender:{Gender.ToString()}, from {Country}, genre:{Genre.ToString()}, size:{Size}, debut:{DebutAlbum}";
        }
    }
}
