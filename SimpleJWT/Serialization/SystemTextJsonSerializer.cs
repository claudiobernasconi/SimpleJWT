using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SimpleJWT.Serialization
{
    public class SystemTextJsonSerializer : IJsonSerializer, IJsonDeserializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public IDictionary<string, object> Deserialize(string json)
        {
            var result = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            var parsedResult = new Dictionary<string, object>();
            foreach (var item in result)
            {
                parsedResult[item.Key] = ConvertJsonElement(item.Value);
            }

            return parsedResult;
        }

        private static object ConvertJsonElement(JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.String)
            {
                // Attempt to parse a DateTime if the value is a string
                if (DateTime.TryParse(jsonElement.GetString(), out var dateValue))
                {
                    return dateValue;
                }
                else
                {
                    return jsonElement.GetString(); // Return as string if not a valid DateTime
                }
            }
            else if (jsonElement.ValueKind == JsonValueKind.Number)
            {
                if (jsonElement.TryGetInt32(out var intValue))
                {
                    return intValue;
                }
                else if (jsonElement.TryGetDecimal(out var decimalValue))
                {
                    return decimalValue;
                }
                else if (jsonElement.TryGetDouble(out var doubleValue))
                {
                    return doubleValue;
                }
                else
                {
                    throw new InvalidOperationException("Unsupported number format");
                }
            }
            else if (jsonElement.ValueKind == JsonValueKind.True || jsonElement.ValueKind == JsonValueKind.False)
            {
                return jsonElement.GetBoolean();
            }
            else if (jsonElement.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            else
            {
                throw new InvalidOperationException("Unsupported JSON value kind");
            }
        }
    }
}
