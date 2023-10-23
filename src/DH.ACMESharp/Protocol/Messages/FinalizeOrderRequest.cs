using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace ACMESharp.Protocol.Messages;

/// <summary>
/// https://tools.ietf.org/html/draft-ietf-acme-acme-12#section-7.3
/// </summary>
public class FinalizeOrderRequest {
    [JsonProperty("csr", Required = Required.Always)]
    [Required]
    public string Csr { get; set; }
}