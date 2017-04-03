using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SimpleJWT
{
	public class JwtEncoder : IJwtEncoder
	{
		private readonly IJsonSerializer _jsonSerializer;

		public JwtEncoder(IJsonSerializer jsonSerializer)
		{
			_jsonSerializer = jsonSerializer;
		}

		public string Encode(IDictionary<string, object> payload, string secret)
		{
			var header = new Header("HS256");

			var headerJsonBase64 = EncodeBase64(_jsonSerializer.Serialize(header));
			var payloadJsonBase64 = EncodeBase64(_jsonSerializer.Serialize(payload));

			var jwt = headerJsonBase64 + "." + payloadJsonBase64;
			var signature = EncodeBase64(Sign(jwt, secret));

			return jwt + "." + signature;
		}

		private string EncodeBase64(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
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
