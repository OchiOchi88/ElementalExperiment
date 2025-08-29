public class RegistUserRepuest
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("token")]
    public string Token { get; set; }
    [JsonProperty("stage")]
    public int Stage { get; set; }
}
