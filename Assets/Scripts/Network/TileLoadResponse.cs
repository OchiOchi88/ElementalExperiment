public class TileLoadResponse
{
    [JsonProperty("x")]
    public int[] TileX { get; set; }
    [JsonProperty("y")]
    public int[] TileY { get; set; }
    [JsonProperty("type")]
    public int[] TileType { get; set; }
}
