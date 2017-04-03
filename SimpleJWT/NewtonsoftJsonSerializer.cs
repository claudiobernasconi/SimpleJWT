using Newtonsoft.Json;

namespace SimpleJWT
{
	public class NewtonsoftJsonSerializer : IJsonSerializer, IJsonDeserializer
	{
		public string Serialize<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
