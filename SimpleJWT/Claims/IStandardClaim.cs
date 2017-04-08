namespace SimpleJWT.Claims
{
    public interface IStandardClaim
    {
        string Key { get; }
        object GetValue();
    }
}
