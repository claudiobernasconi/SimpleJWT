using System.Collections.Generic;

namespace SimpleJWT
{
    public interface IJwtDecoder
    {
        IDictionary<string, object> Decode(string jwt, string secret);
    }
}
