class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a list of words separated by spaces:");
        string inputWords = Console.ReadLine();
        List<string> wordsList = inputWords.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        var stop = false;
        string unwantedLetters = string.Empty;

        while (!stop)
        {
            Console.WriteLine("Enter unwanted letters separated by spaces:");
            unwantedLetters += Console.ReadLine();

            List<string> filtered = EliminateWordsWithUnwantedLetters(wordsList, unwantedLetters);

            Console.WriteLine("Filtered List:");
            foreach (string word in filtered)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("Continue? y/n");

            var q = Console.ReadLine(); // Keep the console window open
            stop = q == "n";
        }

    }

    static List<string> EliminateWordsWithUnwantedLetters(List<string> words, string unwantedLetters)
    {
        List<string> filteredList = new List<string>();

        foreach (string word in words)
        {
            if (!ContainsUnwantedLetters(word, unwantedLetters))
            {
                filteredList.Add(word);
            }
        }

        return filteredList;
    }

    static bool ContainsUnwantedLetters(string word, string unwantedLetters)
    {
        return word.Any(letter => unwantedLetters.Contains(letter, StringComparison.CurrentCultureIgnoreCase));
    }
}