using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJWT.Base64;
using SimpleJWT.Serialization;

namespace SimpleJWT.Test
{
	[TestClass]
	public class JwtDecoderTest
	{
		[TestMethod]
		public void Decode_with_correct_key()
		{
			var jwtDecoder = new JwtDecoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new Base64Encoder());

			const string secret = "secret";
			const string jwt = "eyJUeXAiOiJKV1QiLCJBbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.du+/ve+/vUNj77+977+9Tu+/vdSmUzjvv71UBnFMbe+/vQTvv71b77+9yZ7vv73vv73vv70Y77+9Uw==";

			var payload = jwtDecoder.Decode(jwt, secret);

			Assert.AreEqual("John Doe", payload["name"]);
			Assert.AreEqual("1234567890", payload["sub"]);
			Assert.AreEqual(true, payload["admin"]);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidTokenSignatureException))]
		public void Decode_with_incorrect_key_throws_InavlidTokenSignatureException()
		{
			var jwtDecoder = new JwtDecoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new Base64Encoder());

			const string secret = "wrong-key";
			const string jwt = "eyJUeXAiOiJKV1QiLCJBbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.du+/ve+/vUNj77+977+9Tu+/vdSmUzjvv71UBnFMbe+/vQTvv71b77+9yZ7vv73vv73vv70Y77+9Uw==";

			jwtDecoder.Decode(jwt, secret);
		}
	}
}
