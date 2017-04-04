namespace SimpleJWT.Serialization
{
	public interface IJsonSerializer
	{
		string Serialize<T>(T obj);
	}
}
