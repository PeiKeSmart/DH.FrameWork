using System.Text.Encodings.Web;
using System.Text.Json;
using DH.Payment.Alipay.Parser.JsonConverters;

namespace DH.Payment.Alipay.Parser
{
    public static class JsonParser
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new() { IgnoreNullValues = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, AllowTrailingCommas = true };

        static JsonParser()
        {
            JsonSerializerOptions.Converters.Add(new NumberToStringConverter());
        }
    }
}
