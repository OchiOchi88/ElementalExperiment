public class RegistUserRepuest
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("stage")]
    public int Lvl { get; set; }
    [JsonProperty("exp")]
    public int Exp { get; set; }
    [JsonProperty("clan")]
    public int Clan { get; set; }
}
