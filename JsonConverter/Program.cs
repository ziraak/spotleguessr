// See https://aka.ms/new-console-template for more information
using Models;
using Newtonsoft.Json;

internal class Program
{
    private static readonly string[] africa = new string[] {
        "DZ",
        "AO",
        "BW",
        "BI",
        "CM",
        "CV",
        "CF",
        "TD",
        "KM",
        "YT",
        "CG",
        "CD",
        "BJ",
        "GQ",
        "ET",
        "ER",
        "DJ",
        "GA",
        "GM",
        "GH",
        "GN",
        "CI",
        "KE",
        "LS",
        "LR",
        "LY",
        "MG",
        "MW",
        "ML",
        "MR",
        "MU",
        "MA",
        "MZ",
        "NA",
        "NE",
        "NG",
        "GW",
        "RE",
        "RW",
        "SH",
        "ST",
        "SN",
        "SC",
        "SL",
        "SO",
        "ZA",
        "ZW",
        "SS",
        "EH",
        "SD",
        "SZ",
        "TG",
        "TN",
        "UG",
        "EG",
        "TZ",
        "BF",
        "ZM",
    };

    private static readonly string[] asia = new string[] {
        "AF",
        "AZ",
        "BH",
        "BD",
        "AM",
        "BT",
        "IO",
        "BN",
        "MM",
        "KH",
        "LK",
        "CN",
        "TW",
        "CX",
        "CC",
        "CY",
        "GE",
        "PS",
        "HK",
        "IN",
        "ID",
        "IR",
        "IQ",
        "IL",
        "JP",
        "KZ",
        "JO",
        "KP",
        "KR",
        "KW",
        "KG",
        "LA",
        "LB",
        "MO",
        "MY",
        "MV",
        "MN",
        "OM",
        "NP",
        "PK",
        "PH",
        "TL",
        "QA",
        "RU",
        "SA",
        "SG",
        "VN",
        "SY",
        "TJ",
        "TH",
        "AE",
        "TR",
        "TM",
        "UZ",
        "YE",
        "XE",
        "XD",
        "XS",
    };

    private static readonly string[] europe = new string[] {
    "AL",
    "AD",
    "AZ",
    "AT",
    "AM",
    "BE",
    "BA",
    "BG",
    "BY",
    "HR",
    "CY",
    "CZ",
    "DK",
    "EE",
    "FO",
    "FI",
    "AX",
    "FR",
    "GE",
    "DE",
    "GI",
    "GR",
    "VA",
    "HU",
    "IS",
    "IE",
    "IT",
    "KZ",
    "LV",
    "LI",
    "LT",
    "LU",
    "MT",
    "MC",
    "MD",
    "ME",
    "NL",
    "NO",
    "PL",
    "PT",
    "RO",
    "RU",
    "SM",
    "RS",
    "SK",
    "SI",
    "ES",
    "SJ",
    "SE",
    "CH",
    "TR",
    "UA",
    "MK",
    "GB",
    "GB-ENG",
    "UK",
    "GG",
    "JE",
    "IM",
};

    private static readonly string[] north_america = new string[] {
    "AG",
    "BS",
    "BB",
    "BM",
    "BZ",
    "VG",
    "CA",
    "KY",
    "CR",
    "CU",
    "DM",
    "DO",
    "SV",
    "GL",
    "GD",
    "GP",
    "GT",
    "HT",
    "HN",
    "JM",
    "MQ",
    "MX",
    "MS",
    "AN",
    "CW",
    "AW",
    "SX",
    "BQ",
    "NI",
    "UM",
    "PA",
    "PR",
    "BL",
    "KN",
    "AI",
    "LC",
    "MF",
    "PM",
    "VC",
    "TT",
    "TC",
    "US",
    "VI",
};

    private static readonly string[] oceania = new string[] {
    "AS",
    "AU",
    "SB",
    "CK",
    "FJ",
    "PF",
    "KI",
    "GU",
    "NR",
    "NC",
    "VU",
    "NZ",
    "NU",
    "NF",
    "MP",
    "UM",
    "FM",
    "MH",
    "PW",
    "PG",
    "PN",
    "TK",
    "TO",
    "TV",
    "WF",
    "WS",
    "XX",
};

    private static readonly string[] south_america = new string[] {
    "AR",
    "BO",
    "BR",
    "CL",
    "CO",
    "EC",
    "FK",
    "GF",
    "GY",
    "PY",
    "PE",
    "SR",
    "UY",
    "VE",
};


    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var jsonList = LoadJson();
        var finalList = new List<Artist>();

        foreach (var artist in jsonList)
        {
            finalList.Add(
                new Artist()
                {
                    Name = artist.Artist,
                    Country = artist.Country.ToLower(),
                    DebutAlbum = (int)artist.DebutAlbumYear.GetYear(),
                    Gender = artist.Gender,
                    Genre = artist.Genre,
                    Size = (int)artist.GroupSize,
                    Rank = artist.Index + 1,
                    Continent = GetContinent(artist.Country)

                });
        }

        WriteJsonAsync(finalList);

    }

    public static Continent? GetContinent(string country)
    {
        if (oceania.Contains(country, StringComparer.InvariantCultureIgnoreCase))
            return Continent.Oceania;
        if (africa.Contains(country, StringComparer.InvariantCultureIgnoreCase))
            return Continent.Africa;
        if (north_america.Contains(country, StringComparer.InvariantCultureIgnoreCase))
            return Continent.NorthAmerica;
        if (south_america.Contains(country, StringComparer.InvariantCultureIgnoreCase))
            return Continent.SouthAmerica;
        if (europe.Contains(country, StringComparer.InvariantCultureIgnoreCase))
            return Continent.Europe;
        if (asia.Contains(country, StringComparer.InvariantCultureIgnoreCase))
            return Continent.Asia;

        return null;


    }

    public static void WriteJsonAsync(List<Artist> data)
    {

        string json = JsonConvert.SerializeObject(data.ToArray(), Formatting.Indented);

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