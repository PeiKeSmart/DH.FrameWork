﻿using ACMESharp.Protocol.Resources;

using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace ACMESharp.Protocol.Messages;

/// <summary>
/// https://tools.ietf.org/html/draft-ietf-acme-acme-12#section-7.4
/// </summary>
public class CreateOrderRequest {
    [JsonProperty("identifiers", Required = Required.Always)]
    [Required, MinLength(1)]
    public Identifier[] Identifiers { get; set; }

    [JsonProperty("notBefore", NullValueHandling = NullValueHandling.Ignore)]
    public string NotBefore { get; set; }

    [JsonProperty("notAfter", NullValueHandling = NullValueHandling.Ignore)]
    public string NotAfter { get; set; }
}