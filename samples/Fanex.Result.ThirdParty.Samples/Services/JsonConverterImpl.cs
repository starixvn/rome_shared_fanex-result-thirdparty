namespace Fanex.Result.ThirdParty.Samples.Services
{
    using Newtonsoft.Json;

    public class JsonConverterImpl : ThirdParty.Services.JsonConverter
    {
        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
