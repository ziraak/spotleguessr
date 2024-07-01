using Models;


internal class Program
{
    static bool year, size, gender, genre, nationality;



    private static void Main(string[] args)
    {
        var artistList = LoadJson();
        List<Artist> possibleAnwsers = artistList;



        var isGuessed = false;

        Console.WriteLine("input: h = higher, l = lower, c = correct, f = false, y = yellow (for almost correct, so that would be yh for almost but higher)");

        while (!isGuessed)
        {
            var likely = CountAttributes(possibleAnwsers);

            var chosen = GetGuess(artistList, likely);

            Console.WriteLine(chosen.WriteToConsole());
            Console.WriteLine("was this the band? y/n");
            var q = Console.ReadLine();
            if (q == "y")
            {
                break;
            }


            possibleAnwsers = guessYear(chosen, possibleAnwsers);
            Console.WriteLine(possibleAnwsers.Count() + " possibilities");

            possibleAnwsers = guessSize(chosen, possibleAnwsers);
            Console.WriteLine(possibleAnwsers.Count() + " possibilities");

            possibleAnwsers = guessRank(chosen, possibleAnwsers);
            Console.WriteLine(possibleAnwsers.Count() + " possibilities");

            possibleAnwsers = guessGender(chosen, possibleAnwsers);
            Console.WriteLine(possibleAnwsers.Count() + " possibilities");

            possibleAnwsers = guessGenre(chosen, possibleAnwsers);
            Console.WriteLine(possibleAnwsers.Count() + " possibilities");

            possibleAnwsers = guessNationality(chosen, possibleAnwsers);
            Console.WriteLine(possibleAnwsers.Count() + " possibilities");

        }




        Artist CountAttributes(List<Artist> artists)
        {
            // Create dictionaries to store the count of each unique combination of attributes
            Dictionary<string, int> genderCount = new Dictionary<string, int>();
            Dictionary<string, int> countryCount = new Dictionary<string, int>();
            Dictionary<string, int> genreCount = new Dictionary<string, int>();
            Dictionary<string, int> sizeCount = new Dictionary<string, int>();
            Dictionary<string, int> debutDecadeCount = new Dictionary<string, int>();
            Dictionary<string, int> continentCount = new Dictionary<string, int>();
            Dictionary<string, int> RankSectionCount = new Dictionary<string, int>();

            foreach (var artist in artists)
            {
                // Extract attributes for each artist
                var genderKey = artist.Gender.ToString();
                var countryKey = artist.Country;
                var genreKey = artist.Genre.ToString();
                var sizeKey = artist.Size.ToString();
                var debutDecadeKey = artist.GetDebutAlbumDecade();
                var continentKey = artist.Continent.ToString();
                var rankSectionKey = artist.GetRankSection();

                // Update count for each attribute
                UpdateCount(genderCount, genderKey);
                UpdateCount(countryCount, countryKey);
                UpdateCount(genreCount, genreKey);
                UpdateCount(sizeCount, sizeKey);
                UpdateCount(debutDecadeCount, debutDecadeKey);
                UpdateCount(continentCount, continentKey);
                UpdateCount(RankSectionCount, rankSectionKey);
            }

            foreach (var artist in artists)
            {
                artist.Score = 0;
                // Extract attributes for each artist
                string genderKey = artist.Gender.ToString();
                string countryKey = artist.Country;
                string genreKey = artist.Genre.ToString();
                string sizeKey = artist.Size.ToString();
                string debutDecadeKey = artist.GetDebutAlbumDecade();
                string continentKey = artist.Continent.ToString();
                var rankSectionKey = artist.GetRankSection();

                // Update count for each attribute
                UpdateScore(genderCount, genderKey, artist);
                UpdateScore(countryCount, countryKey, artist);
                UpdateScore(genreCount, genreKey, artist);
                UpdateScore(sizeCount, sizeKey, artist);
                UpdateScore(debutDecadeCount, debutDecadeKey, artist);
                UpdateScore(continentCount, continentKey, artist);
                UpdateScore(RankSectionCount, rankSectionKey, artist);
            }

            var ScoredArtists = artists.OrderByDescending(obj => obj.Score);
            Console.WriteLine(string.Join(", ", ScoredArtists.Select(x => x.Name + ":" + x.Score)));

            var mostLikely = ScoredArtists.FirstOrDefault();
            Console.WriteLine(mostLikely.Name);

            return mostLikely;
        }



        // Function to update count in the dictionary
        void UpdateScore(Dictionary<string, int> dictionary, string key, Artist artist)
        {
            if (dictionary.ContainsKey(key))
            {
                artist.Score += dictionary[key];
            }
        }


        // Function to update count in the dictionary
        void UpdateCount(Dictionary<string, int> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key]++;
            }
            else
            {
                dictionary[key] = 1;
            }
        }


        List<Artist> LoadJson()
        {
            using (StreamReader r = new StreamReader("../../../../artists2.json"))
            {
                string json = r.ReadToEnd();
                var items = Artist.FromJson(json);
                return items;
            }
        }

        List<Artist> guessYear(Artist chosenBand, List<Artist> possibleAnswers)
        {
            if (year) return possibleAnswers;

            Console.WriteLine($"was year {chosenBand.DebutAlbum} (y) h/l/c");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "c":
                    possibleAnswers = possibleAnswers.Where(x => x.DebutAlbum == chosenBand.DebutAlbum).ToList();
                    year = true;
                    break;
                case "h":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.DebutAlbum + 5 < x.DebutAlbum).ToList();
                    break;
                case "l":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.DebutAlbum - 5 > x.DebutAlbum).ToList();
                    break;
                case "yh":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.DebutAlbum < x.DebutAlbum && chosenBand.DebutAlbum + 5 >= x.DebutAlbum).ToList();
                    break;
                case "yl":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.DebutAlbum > x.DebutAlbum && chosenBand.DebutAlbum - 5 <= x.DebutAlbum).ToList();
                    break;
                default:
                    return guessYear(chosenBand, possibleAnwsers);
            }

            return possibleAnswers;
        }
        List<Artist> guessSize(Artist chosenBand, List<Artist> possibleAnswers)
        {
            if (size) return possibleAnswers;
            Console.WriteLine($"was bandsize {chosenBand.Size} f/c");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "c":
                    possibleAnswers = possibleAnswers.Where(x => x.Size == chosenBand.Size).ToList();
                    size = true;
                    break;
                case "f":
                    possibleAnswers = possibleAnswers.Where(x => x.Size != chosenBand.Size).ToList();
                    break;
                default:
                    return guessSize(chosenBand, possibleAnwsers);
            }

            return possibleAnswers;
        }
        List<Artist> guessGender(Artist chosenBand, List<Artist> possibleAnswers)
        {
            if (gender) return possibleAnswers;
            Console.WriteLine($"was gender {chosenBand.Gender} f/c");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "c":
                    possibleAnswers = possibleAnswers.Where(x => x.Gender == chosenBand.Gender).ToList();
                    gender = true;
                    break;
                case "f":
                    possibleAnswers = possibleAnswers.Where(x => x.Gender != chosenBand.Gender).ToList();
                    break;
                default:
                    return guessGender(chosenBand, possibleAnwsers);
            }

            return possibleAnswers;
        }

        List<Artist> guessGenre(Artist chosenBand, List<Artist> possibleAnswers)
        {
            if (genre) return possibleAnswers;
            Console.WriteLine($"was genre {chosenBand.Genre} f/c");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "c":
                    possibleAnswers = possibleAnswers.Where(x => x.Genre == chosenBand.Genre).ToList();
                    genre = true;
                    break;
                case "f":
                    possibleAnswers = possibleAnswers.Where(x => x.Genre != chosenBand.Genre).ToList();
                    break;
                default:
                    return guessGenre(chosenBand, possibleAnwsers);
            }

            return possibleAnswers;
        }
        List<Artist> guessNationality(Artist chosenBand, List<Artist> possibleAnswers)
        {
            if (nationality) return possibleAnswers;
            Console.WriteLine($"was country {chosenBand.Country} y/f/c");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "c":
                    possibleAnswers = possibleAnswers.Where(x => x.Country == chosenBand.Country).ToList();
                    nationality = true;
                    break;
                case "f":
                    possibleAnswers = possibleAnswers.Where(x => x.Country != chosenBand.Country && x.Continent != chosenBand.Continent).ToList();
                    break;
                case "y":
                    possibleAnswers = possibleAnswers.Where(x => x.Country != chosenBand.Country && x.Continent == chosenBand.Continent).ToList();
                    break;
                default:
                    return guessNationality(chosenBand, possibleAnwsers);
            }

            return possibleAnswers;
        }

        List<Artist> guessRank(Artist chosenBand, List<Artist> possibleAnswers)
        {
            Console.WriteLine($"was rank {chosenBand.Rank} (y) h/l/c");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "c":
                    possibleAnswers = possibleAnswers.Where(x => x.Rank == chosenBand.Rank).ToList();
                    break;
                case "h":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.Rank - 50 > x.Rank).ToList();
                    break;
                case "l":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.Rank + 50 < x.Rank).ToList();
                    break;
                case "yh":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.Rank > x.Rank && chosenBand.Rank - 50 <= x.Rank).ToList();
                    break;
                case "yl":
                    possibleAnswers = possibleAnswers.Where(x => chosenBand.Rank < x.Rank && chosenBand.Rank + 50 >= x.Rank).ToList();
                    break;
                default:
                    return guessRank(chosenBand, possibleAnwsers);
            }

            return possibleAnswers;
        }

        Artist? GetGuess(List<Artist> newModels, Artist mostLikely)
        {
            while (true)
            {
                Console.WriteLine("Type your guess, type l for most likely");

                Console.Out.Flush();
                var name = Console.ReadLine();
                if (name == "l")
                {
                    return mostLikely;
                }

                var newModel = newModels.SingleOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

                if (newModel == null) continue;

                return newModel;
                break;
            }
        }
    }
}