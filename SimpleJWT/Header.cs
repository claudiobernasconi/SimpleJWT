namespace SimpleJWT
{
	internal class Header
	{
		public Header(string alg)
		{
			Typ = "JWT";
			Alg = alg;
		}

		public string Typ { get; private set; }
		public string Alg { get; private set; }
	}
}