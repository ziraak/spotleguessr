using Models;


var artistList = LoadJson();
List<NewModel> possibleAnwsers = artistList;
var isGuessed = false;

Console.WriteLine("input: h = higher, l = lower, c = correct, f = false, y = yellow (for almost correct, so that would be yh for almost but higher)");

while (!isGuessed)
{

    var chosen = GetGuess(artistList);

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
    Console.WriteLine(string.Join(", ", possibleAnwsers.Select(x => x.Name)));
}






List<NewModel> LoadJson()
{
    using (StreamReader r = new StreamReader("../../../../artists2.json"))
    {
        string json = r.ReadToEnd();
        var items = NewModel.FromJson(json);
        return items;
    }
}

List<NewModel> guessYear(NewModel chosenBand, List<NewModel> possibleAnswers)
{
    Console.WriteLine($"was year {chosenBand.DebutAlbum} (y) h/l/c");
    var answer = Console.ReadLine();
    switch (answer)
    {
        case "c":
            possibleAnswers = possibleAnswers.Where(x => x.DebutAlbum == chosenBand.DebutAlbum).ToList();
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
List<NewModel> guessSize(NewModel chosenBand, List<NewModel> possibleAnswers)
{
    Console.WriteLine($"was bandsize {chosenBand.Size} f/c");
    var answer = Console.ReadLine();
    switch (answer)
    {
        case "c":
            possibleAnswers = possibleAnswers.Where(x => x.Size == chosenBand.Size).ToList();
            break;
        case "f":
            possibleAnswers = possibleAnswers.Where(x => x.Size != chosenBand.Size).ToList();
            break;
        default:
            return guessSize(chosenBand, possibleAnwsers);
    }

    return possibleAnswers;
}
List<NewModel> guessGender(NewModel chosenBand, List<NewModel> possibleAnswers)
{
    Console.WriteLine($"was gender {chosenBand.Gender} f/c");
    var answer = Console.ReadLine();
    switch (answer)
    {
        case "c":
            possibleAnswers = possibleAnswers.Where(x => x.Gender == chosenBand.Gender).ToList();
            break;
        case "f":
            possibleAnswers = possibleAnswers.Where(x => x.Gender != chosenBand.Gender).ToList();
            break;
        default:
            return guessGender(chosenBand, possibleAnwsers);
    }

    return possibleAnswers;
}

List<NewModel> guessGenre(NewModel chosenBand, List<NewModel> possibleAnswers)
{
    Console.WriteLine($"was genre {chosenBand.Genre} f/c");
    var answer = Console.ReadLine();
    switch (answer)
    {
        case "c":
            possibleAnswers = possibleAnswers.Where(x => x.Genre == chosenBand.Genre).ToList();
            break;
        case "f":
            possibleAnswers = possibleAnswers.Where(x => x.Genre != chosenBand.Genre).ToList();
            break;
        default:
            return guessGenre(chosenBand, possibleAnwsers);
    }

    return possibleAnswers;
}
List<NewModel> guessNationality(NewModel chosenBand, List<NewModel> possibleAnswers)
{
    Console.WriteLine($"was country {chosenBand.Country} f/c");
    var answer = Console.ReadLine();
    switch (answer)
    {
        case "c":
            possibleAnswers = possibleAnswers.Where(x => x.Country == chosenBand.Country).ToList();
            break;
        case "f":
            possibleAnswers = possibleAnswers.Where(x => x.Country != chosenBand.Country).ToList();
            break;
        default:
            return guessNationality(chosenBand, possibleAnwsers);
    }

    return possibleAnswers;
}

List<NewModel> guessRank(NewModel chosenBand, List<NewModel> possibleAnswers)
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

NewModel? GetGuess(List<NewModel> newModels)
{
    while (true)
    {
        Console.WriteLine("Type your guess");

        Console.Out.Flush();
        var name = Console.ReadLine();

        var newModel = newModels.SingleOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (newModel == null) continue;

        return newModel;
        break;
    }
}