namespace Fanex.Result.ThirdParty.Services
{
    public interface JsonConverter
    {
        string SerializeObject(object value);

        T DeserializeObject<T>(string value);
    }
}
