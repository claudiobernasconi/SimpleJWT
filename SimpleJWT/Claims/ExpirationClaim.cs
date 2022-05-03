using System;

namespace SimpleJWT.Claims
{
    public class ExpirationClaim : IStandardClaim
    {
        private readonly int _expirationInMinutes;

        public ExpirationClaim(int expirationInMinutes = 30)
        {
            _expirationInMinutes = expirationInMinutes;
        }

        public string Key
        {
            get { return "exp"; }
        }

        public object GetValue()
        {
            return DateTime.UtcNow.AddMinutes(_expirationInMinutes).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
