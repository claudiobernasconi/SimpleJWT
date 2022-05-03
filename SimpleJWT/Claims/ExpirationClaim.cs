using System;

namespace SimpleJWT.Claims
{
    internal class ExpirationClaim : IStandardClaim
    {
        private readonly int expirationInMinutes;

        public ExpirationClaim(int expirationInMinutes = 30)
        {
            this.expirationInMinutes = expirationInMinutes;
        }

        public string Key
        {
            get { return "exp"; }
        }

        public object GetValue()
        {
            return DateTime.UtcNow.AddMinutes(expirationInMinutes).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
