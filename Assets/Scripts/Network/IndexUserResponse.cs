public class IndexUserResponse 
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("stage")]
    public int Stage { get; set; }
    [JsonProperty("achievement")]
    public int Achievement { get; set; }
}
