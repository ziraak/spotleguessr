
namespace Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public partial class JsonModel
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("genre")]
        public Genre Genre { get; set; }


        [JsonProperty("debut_album_year")]
        public DebutAlbumYear DebutAlbumYear { get; set; }



        [JsonProperty("group_size")]
        public long GroupSize { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        public string GetArtistName() => Artist.Split('(').FirstOrDefault().Trim();

        public Gender GetGender()
        {
            switch (Gender)
            {
                case Gender.GenderM:
                case Gender.M:
                    return Gender.M;
                case Gender.X:
                case Gender.GenderX:
                    return Gender.X;
                default:
                    return Gender;
            }
        }

    }

    public enum Gender { F, GenderM, GenderX, M, Nb, X };

    public enum Genre { African, Alternative, Classical, Country, Electronic, FixLater, Folk, HipHop, Jazz, Latin, Metal, Pop, RB, Reggae, Rock };

    public partial struct DebutAlbumYear
    {
        public DateTimeOffset? DateTime;
        public long? Integer;

        public int GetYear()
        {
            if (this.DateTime == null)
            {
                return (int)Integer;
            }
            return DateTime.Value.Year;
        }

        public static implicit operator DebutAlbumYear(DateTimeOffset DateTime) => new DebutAlbumYear { DateTime = DateTime };
        public static implicit operator DebutAlbumYear(long Integer) => new DebutAlbumYear { Integer = Integer };
    }


    public partial class JsonModel
    {
        public static List<JsonModel> FromJson(string json) => JsonConvert.DeserializeObject<List<JsonModel>>(json, Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<JsonModel> self) => JsonConvert.SerializeObject(self, Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                DebutAlbumYearConverter.Singleton,
                GenderConverter.Singleton,
                GenreConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class DebutAlbumYearConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DebutAlbumYear) || t == typeof(DebutAlbumYear?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new DebutAlbumYear { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    DateTimeOffset dt;
                    if (DateTimeOffset.TryParse(stringValue, out dt))
                    {
                        return new DebutAlbumYear { DateTime = dt };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type DebutAlbumYear");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (DebutAlbumYear)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.DateTime != null)
            {
                serializer.Serialize(writer, value.DateTime.Value.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
                return;
            }
            throw new Exception("Cannot marshal type DebutAlbumYear");
        }

        public static readonly DebutAlbumYearConverter Singleton = new DebutAlbumYearConverter();
    }

    internal class GenderConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Gender) || t == typeof(Gender?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "M":
                    return Gender.GenderM;
                case "X":
                    return Gender.GenderX;
                case "f":
                    return Gender.F;
                case "m":
                    return Gender.M;
                case "nb":
                    return Gender.Nb;
                case "x":
                    return Gender.X;
            }
            throw new Exception("Cannot unmarshal type Gender");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Gender)untypedValue;
            switch (value)
            {
                case Gender.GenderM:
                    serializer.Serialize(writer, "M");
                    return;
                case Gender.GenderX:
                    serializer.Serialize(writer, "X");
                    return;
                case Gender.F:
                    serializer.Serialize(writer, "f");
                    return;
                case Gender.M:
                    serializer.Serialize(writer, "m");
                    return;
                case Gender.Nb:
                    serializer.Serialize(writer, "nb");
                    return;
                case Gender.X:
                    serializer.Serialize(writer, "x");
                    return;
            }
            throw new Exception("Cannot marshal type Gender");
        }

        public static readonly GenderConverter Singleton = new GenderConverter();
    }

    internal class GenreConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Genre) || t == typeof(Genre?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "African":
                    return Genre.African;
                case "Alternative":
                    return Genre.Alternative;
                case "Classical":
                    return Genre.Classical;
                case "Country":
                    return Genre.Country;
                case "Electronic":
                    return Genre.Electronic;
                case "Fix Later":
                    return Genre.FixLater;
                case "Folk":
                    return Genre.Folk;
                case "Hip Hop":
                    return Genre.HipHop;
                case "Jazz":
                    return Genre.Jazz;
                case "Latin":
                    return Genre.Latin;
                case "Metal":
                    return Genre.Metal;
                case "Pop":
                    return Genre.Pop;
                case "R&B":
                    return Genre.RB;
                case "Reggae":
                    return Genre.Reggae;
                case "Rock":
                    return Genre.Rock;
            }
            throw new Exception("Cannot unmarshal type Genre");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Genre)untypedValue;
            switch (value)
            {
                case Genre.African:
                    serializer.Serialize(writer, "African");
                    return;
                case Genre.Alternative:
                    serializer.Serialize(writer, "Alternative");
                    return;
                case Genre.Classical:
                    serializer.Serialize(writer, "Classical");
                    return;
                case Genre.Country:
                    serializer.Serialize(writer, "Country");
                    return;
                case Genre.Electronic:
                    serializer.Serialize(writer, "Electronic");
                    return;
                case Genre.FixLater:
                    serializer.Serialize(writer, "Fix Later");
                    return;
                case Genre.Folk:
                    serializer.Serialize(writer, "Folk");
                    return;
                case Genre.HipHop:
                    serializer.Serialize(writer, "Hip Hop");
                    return;
                case Genre.Jazz:
                    serializer.Serialize(writer, "Jazz");
                    return;
                case Genre.Latin:
                    serializer.Serialize(writer, "Latin");
                    return;
                case Genre.Metal:
                    serializer.Serialize(writer, "Metal");
                    return;
                case Genre.Pop:
                    serializer.Serialize(writer, "Pop");
                    return;
                case Genre.RB:
                    serializer.Serialize(writer, "R&B");
                    return;
                case Genre.Reggae:
                    serializer.Serialize(writer, "Reggae");
                    return;
                case Genre.Rock:
                    serializer.Serialize(writer, "Rock");
                    return;
            }
            throw new Exception("Cannot marshal type Genre");
        }

        public static readonly GenreConverter Singleton = new GenreConverter();
    }
}
