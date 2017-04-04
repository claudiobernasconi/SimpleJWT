using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SimpleJWT.Base64;

namespace SimpleJWT
{
	public class JwtDecoder : IJwtDecoder
	{
		private readonly IJsonDeserializer _jsonDeserializer;
		private readonly IBase64Encoder _base64Encoder;
		private readonly IBase64Decoder _base64Decoder;

		public JwtDecoder(IJsonDeserializer jsonDeserializer, IBase64Encoder base64Encoder, IBase64Decoder base64Decoder)
		{
			_jsonDeserializer = jsonDeserializer;
			_base64Encoder = base64Encoder;
			_base64Decoder = base64Decoder;
		}

		public IDictionary<string, object> Decode(string jwt, string secret)
		{
			var parts = jwt.Split('.');
			if (parts.Length != 3)
			{
				throw new Exception(string.Format("Incorrect token count. Expected 3, received : {0}", parts.Length));
			}

			var header = _jsonDeserializer.Deserialize<Header>(_base64Decoder.Decode(parts[0]));
			var payload = _jsonDeserializer.Deserialize<IDictionary<string, object>>(_base64Decoder.Decode(parts[1]));
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
			return signature == _base64Encoder.Encode(Sign(rawSignature, secret));
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
