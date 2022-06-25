using System.Text.Json.Serialization;
// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Name
    {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }
    }

    public class RootNames
    {
        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }

