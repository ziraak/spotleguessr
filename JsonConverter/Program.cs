// See https://aka.ms/new-console-template for more information
using Models;
using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var jsonList = LoadJson();
        var finalList = new List<NewModel>();

        for (int i = 0; i < jsonList.Length; i++)
        {
            var model = jsonList[i];
            finalList.Add(
                new NewModel()
                {
                    Name = model.Artist,
                    Country = model.Country,
                    DebutAlbum = (int)model.DebutAlbumYear,
                    Gender = model.Gender,
                    Genre = model.Genre,
                    Size = (int)model.GroupSize,
                    Rank = i + 1

                });
        }

        WriteJsonAsync(finalList);

    }

    public static void WriteJsonAsync(List<NewModel> data)
    {

        string json = JsonConvert.SerializeObject(data.ToArray());

        //write string to file
        System.IO.File.WriteAllText(@"../../../../artists2.json", json);

    }
    public static JsonModel[] LoadJson()
    {
        using (StreamReader r = new StreamReader("../../../../artists.json"))
        {
            string json = r.ReadToEnd();
            JsonModel[] items = JsonModel.FromJson(json).ToArray();
            return items;
        }
    }
}