using System;

namespace SimpleJWT.Exceptions
{
    public class InvalidTokenSignatureException : Exception
    {
        public InvalidTokenSignatureException()
        {
        }

        public InvalidTokenSignatureException(string message)
        : base(message)
        {
        }

        public InvalidTokenSignatureException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
