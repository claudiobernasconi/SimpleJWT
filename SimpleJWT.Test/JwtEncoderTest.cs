using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJWT.Base64;

namespace SimpleJWT.Test
{
	[TestClass]
	public class JwtEncoderTest
	{
		[TestMethod]
		public void Test()
		{
			var jwtEncoder = new JwtEncoder(new NewtonsoftJsonSerializer(), new Base64Encoder());

			var payload = new Dictionary<string, object>
			{
				{"sub", "1234567890"}, 
				{"name", "John Doe"}, 
				{"admin", true}
			};

			const string secret = "secret";

			var jwt = jwtEncoder.Encode(payload, secret);
			Assert.AreEqual("eyJUeXAiOiJKV1QiLCJBbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.du+/ve+/vUNj77+977+9Tu+/vdSmUzjvv71UBnFMbe+/vQTvv71b77+9yZ7vv73vv73vv70Y77+9Uw==", jwt);
		}
	}
}
