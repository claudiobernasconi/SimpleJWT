namespace SimpleJWT
{
	public interface IJsonSerializer
	{
		string Serialize<T>(T obj);
	}
}
