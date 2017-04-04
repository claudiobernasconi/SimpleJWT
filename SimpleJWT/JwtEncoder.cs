using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SimpleJWT.Base64;

namespace SimpleJWT
{
	public class JwtEncoder : IJwtEncoder
	{
		private readonly IJsonSerializer _jsonSerializer;
		private readonly IBase64Encoder _base64Encoder;

		public JwtEncoder(IJsonSerializer jsonSerializer, IBase64Encoder base64Encoder)
		{
			_jsonSerializer = jsonSerializer;
			_base64Encoder = base64Encoder;
		}

		public string Encode(IDictionary<string, object> payload, string secret)
		{
			var header = new Header("HS256");

			var headerJsonBase64 = _base64Encoder.Encode(_jsonSerializer.Serialize(header));
			var payloadJsonBase64 = _base64Encoder.Encode(_jsonSerializer.Serialize(payload));

			var jwt = headerJsonBase64 + "." + payloadJsonBase64;
			var signature = _base64Encoder.Encode(Sign(jwt, secret));

			return jwt + "." + signature;
		}

		private string Sign(string jwt, string secret)
		{
			var secretByteArray = Encoding.UTF8.GetBytes(secret);
			var jwtByteArray = Encoding.UTF8.GetBytes(jwt);

			using (var hmac = new HMACSHA256(secretByteArray))
			{
				return Encoding.UTF8.GetString(hmac.ComputeHash(jwtByteArray));
			}
		}
	}
}
