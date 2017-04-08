using System;

namespace SimpleJWT.Claims
{
    internal class ExpirationClaim : IStandardClaim
    {
        public string Key
        {
            get { return "exp"; }
        }

        public object GetValue()
        {
            return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
