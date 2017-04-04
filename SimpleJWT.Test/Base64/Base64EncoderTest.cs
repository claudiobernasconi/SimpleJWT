using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJWT.Base64;

namespace SimpleJWT.Test.Base64
{
	[TestClass]
	public class Base64EncoderTest
	{
		[TestMethod]
		public void Encode_Test()
		{
			var base64Encoder = new Base64Encoder();

			const string input = "Roger Federer";
			var output = base64Encoder.Encode(input);

			Assert.AreEqual("Um9nZXIgRmVkZXJlcg==", output);
		}

		[TestMethod]
		public void Decode_Test()
		{
			var base64Encoder = new Base64Encoder();

			const string input = "Um9nZXIgRmVkZXJlcg==";
			var output = base64Encoder.Decode(input);

			Assert.AreEqual("Roger Federer", output);
		}
	}
}
