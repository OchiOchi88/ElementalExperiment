using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class UpdateUserRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("lvl")]
    public int Lvl { get; set; }
    [JsonProperty("exp")]
    public int Exp { get; set; }
    [JsonProperty("clan")]
    public int Clan { get; set; }
}
