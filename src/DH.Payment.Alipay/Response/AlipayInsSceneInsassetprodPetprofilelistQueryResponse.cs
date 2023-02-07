using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsSceneInsassetprodPetprofilelistQueryResponse.
    /// </summary>
    public class AlipayInsSceneInsassetprodPetprofilelistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 列表
        /// </summary>
        [JsonPropertyName("pet_profiles")]
        public List<PetProfiles> PetProfiles { get; set; }
    }
}
