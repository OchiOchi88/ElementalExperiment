public class ElementLoadResponse 
{
    [JsonProperty("x")]
    public int[] ElementX { get; set; }
    [JsonProperty("y")]
    public int[] ElementY { get; set; }
    [JsonProperty("type")]
    public int[] ElementType { get; set; }
}
