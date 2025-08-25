public class RegistUserRepuest
{
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("stage")]
    public int stage { get; set; }
    [JsonProperty("achievement")]
    public int achievement { get; set; }
    [JsonProperty("token")]
    public int APITtoken { get; set; }
}
