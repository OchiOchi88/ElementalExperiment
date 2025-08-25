using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveData
{
    [JsonProperty("userName")]
    public string UserName { get; set; }
    [JsonProperty("lvl")]
    public int Lvl { get; set; }
    [JsonProperty("apiToken")]
    public string APIToken { get; set; }
}
