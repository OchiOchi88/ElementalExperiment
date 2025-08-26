using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveData
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("token")]
    public string Token { get; set; }
    //[JsonProperty("stage")]
    //public int Stage { get; set; }
    //[JsonProperty("achievement")]
    //public int Achievement { get; set; }
}
