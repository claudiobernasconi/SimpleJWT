using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SimpleJWT
{
	public class JwtDecoder : IJwtDecoder
	{
		private readonly IJsonDeserializer _jsonDeserializer;

		public JwtDecoder(IJsonDeserializer jsonDeserializer)
		{
			_jsonDeserializer = jsonDeserializer;
		}

		public IDictionary<string, object> Decode(string jwt, string secret)
		{
			var parts = jwt.Split('.');
			if (parts.Length != 3)
			{
				throw new Exception(string.Format("Incorrect token count. Expected 3, received : {0}", parts.Length));
			}

			var header = _jsonDeserializer.Deserialize<Header>(DecodeBase64(parts[0]));
			var payload = _jsonDeserializer.Deserialize<IDictionary<string, object>>(DecodeBase64(parts[1]));
			var signature = parts[2];

			var rawSignature = parts[0] + "." + parts[1];
			if (!VerifySignature(rawSignature, secret, signature))
			{
				throw new Exception("Signature vertification failed");
			}

			return payload;
		}

		private bool VerifySignature(string rawSignature, string secret, string signature)
		{
			return signature == EncodeBase64(Sign(rawSignature, secret));
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

		private string EncodeBase64(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

		private string DecodeBase64(string base64Encoded)
		{
			var plainTextBytes = Convert.FromBase64String(base64Encoded);
			return Encoding.UTF8.GetString(plainTextBytes);
		}
	}
}
