using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveData
{
    [JsonProperty("userName")]
    public string UserName { get; set; }
    [JsonProperty("stage")]
    public int Stage { get; set; }
    [JsonProperty("achievement")]
    public int Achievement { get; set; }
    [JsonProperty("token")]
    public string APIToken { get; set; }
}
