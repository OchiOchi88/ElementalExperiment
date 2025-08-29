using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class UpdateUserRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("stage")]
    public int Stage { get; set; }
}
