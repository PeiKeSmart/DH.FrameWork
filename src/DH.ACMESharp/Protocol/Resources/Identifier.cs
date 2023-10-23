﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ACMESharp.Protocol.Resources;

public class Identifier {
    [JsonProperty("type", Required = Required.Always)]
    [Required]
    public string Type { get; set; }

    [JsonProperty("value", Required = Required.Always)]
    [Required]
    public string Value { get; set; }
}