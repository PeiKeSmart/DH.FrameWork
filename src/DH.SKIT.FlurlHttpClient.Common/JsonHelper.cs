using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SKIT.FlurlHttpClient.Common
{
    public class JsonHelper
    {
        private static readonly JsonSerializerSettings defaultJsonSerializerSettings =
             new JsonSerializerSettings
             {
                 Formatting = Formatting.None,
                 Converters = new IsoDateTimeConverter[] { new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff" } }
             };

        private static readonly JsonSerializerSettings ignoreNullJsonSerializerSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                Converters = new IsoDateTimeConverter[] { new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff" } }
            };

        public static string ToJson<T>(T t)
        {
            string result = "";
            try
            {
                result = JsonConvert.SerializeObject(t, ignoreNullJsonSerializerSettings);
            }
            catch (Exception ex)
            {
                Console.WriteLine("对象" + t.ToString() + "转JSON时出错：" + ex.ToString());
            }
            return result;
        }

        internal static T? ToObject<T>(string value)
        {
            try
            {
                return (String.IsNullOrEmpty(value) || value == "null") ? default(T) : JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
                {
                    Error = delegate (object? obj, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                    {
                        Console.WriteLine(args.ErrorContext.Error.ToString());
                        args.ErrorContext.Handled = true;
                    },
                    Converters = { new IsoDateTimeConverter() }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("JSON转换对象时出错：" + ex.ToString() + "\r\nJson源：" + value + "\r\n");
                throw ex;
            }
        }
    }
}
