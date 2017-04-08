using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SimpleJWT.Base64;
using SimpleJWT.Serialization;
using SimpleJWT.Claims;

namespace SimpleJWT
{
	public class JwtEncoder : IJwtEncoder
	{
		private readonly IJsonSerializer _jsonSerializer;
		private readonly IBase64Encoder _base64Encoder;
        private readonly IEnumerable<IStandardClaim> _standardClaims;

        public JwtEncoder()
        {
            _jsonSerializer = new NewtonsoftJsonSerializer();
            _base64Encoder = new Base64Encoder();
            _standardClaims = new List<IStandardClaim>() { new ExpirationClaim() };
        }

		public JwtEncoder(IJsonSerializer jsonSerializer, IBase64Encoder base64Encoder, IEnumerable<IStandardClaim> standardClaims)
		{
			_jsonSerializer = jsonSerializer;
			_base64Encoder = base64Encoder;
            _standardClaims = standardClaims;
		}

		public string Encode(IDictionary<string, object> payload, string secret)
		{
			var header = new Header("HS256");

            foreach( var standardClaim in _standardClaims)
            {
                payload.Add(new KeyValuePair<string, object>(standardClaim.Key, standardClaim.GetValue()));
            }

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
