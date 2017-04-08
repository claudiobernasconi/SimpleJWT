using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJWT.Base64;
using SimpleJWT.Serialization;
using SimpleJWT.Claims;

namespace SimpleJWT.Test
{
    [TestClass]
    public class JwtEncoderTest
    {
        const string secret = "secret";
        private Dictionary<string, object> _payload = new Dictionary<string, object>
        {
            {"sub", "1234567890"},
            {"name", "John Doe"},
            {"admin", true}
        };

        [TestMethod]
        public void Encode_Token()
        {
            var jwtEncoder = new JwtEncoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new List<IStandardClaim>());

            var jwt = jwtEncoder.Encode(_payload, secret);
            Assert.AreEqual("eyJUeXAiOiJKV1QiLCJBbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.du+/ve+/vUNj77+977+9Tu+/vdSmUzjvv71UBnFMbe+/vQTvv71b77+9yZ7vv73vv73vv70Y77+9Uw==", jwt);
        }

        [TestMethod]
        public void StandardClaims_Are_Included()
        {
            var testClaim = new TestClaim();
            var jwtEncoder = new JwtEncoder(new NewtonsoftJsonSerializer(), new Base64Encoder(), new List<IStandardClaim>() { testClaim });

            var jwt = jwtEncoder.Encode(_payload, secret);
            Assert.IsTrue(testClaim.WasCalled);
        }

        private class TestClaim : IStandardClaim
        {
            public string Key
            {
                get { return "none"; }
            }

            public bool WasCalled { get; set; }

            public object GetValue()
            {
                WasCalled = true;
                return "value";
            }
        }
    }
}
