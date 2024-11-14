using System.Collections.Generic;

namespace SimpleJWT.Serialization
{
    public interface IJsonDeserializer
    {
        IDictionary<string, object> Deserialize(string json);
    }
}