public class RegistUserRepuest
{
    [JsonProperty("name")]
    public string name { get; set; }
    //[JsonProperty("token")]
    //public string Token { get; set; }
    [JsonProperty("stage")]
    public int stage { get; set; }
}
