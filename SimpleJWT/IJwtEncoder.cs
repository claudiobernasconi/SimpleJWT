using System.Collections.Generic;

namespace SimpleJWT
{
	public interface IJwtEncoder
	{
		string Encode(IDictionary<string, object> payload, string secret);
	}
}
