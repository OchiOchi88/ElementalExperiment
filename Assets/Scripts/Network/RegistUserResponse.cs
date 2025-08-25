public class RegistUserResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("token")]
    public string APIToken { get; set; }
}

