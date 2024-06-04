using Newtonsoft.Json;

namespace Models
{
    public class Artist
    {
        public int Score { get; set; }


        public int Rank { get; set; }
        public int Size { get; set; }

        public int DebutAlbum { get; set; }

        public Genre? Genre { get; set; }

        public string Country { get; set; }

        public Gender? Gender { get; set; }
        public string Name { get; set; }
        public Continent? Continent { get; set; }

        public static List<Artist> FromJson(string json) => JsonConvert.DeserializeObject<List<Artist>>(json);

        public string WriteToConsole()
        {
            return $"name: {Name}, rank:{Rank}, gender:{Gender.ToString()}, from {Country}, genre:{Genre.ToString()}, size:{Size}, debut:{DebutAlbum}";
        }

        public string GetDebutAlbumDecade()
        {
            if (DebutAlbum > 0)
            {
                int decade = (DebutAlbum - 1) / 10 * 10; // Calculate the decade
                return $"{decade}s";
            }
            else
            {
                return "Unknown"; // Handle the case where DebutAlbum is not set
            }
        }
    }


    public enum Continent
    {
        Africa,
        Oceania,
        Europe,
        NorthAmerica,
        SouthAmerica,
        Asia
    }
}
