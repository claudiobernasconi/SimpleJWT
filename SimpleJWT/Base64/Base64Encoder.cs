using System;
using System.Text;

namespace SimpleJWT.Base64
{
	public class Base64Encoder : IBase64Encoder, IBase64Decoder
	{
		public string Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

		public string Decode(string base64Encoded)
		{
			var plainTextBytes = Convert.FromBase64String(base64Encoded);
			return Encoding.UTF8.GetString(plainTextBytes);
		}
	}
}
