using System;
using System.Runtime.Serialization;

namespace IisManagementTools
{
    [Serializable]
    internal class UsernamePasswordException : Exception
    {
        public UsernamePasswordException() : base("Username and password cannot both be blank or null. You have to provide at least one of two values.")
        {
        }

        public UsernamePasswordException(string message) : base(message)
        {
        }

        public UsernamePasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsernamePasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}