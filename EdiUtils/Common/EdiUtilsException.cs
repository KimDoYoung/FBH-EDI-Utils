using System;
using System.Runtime.Serialization;

namespace EdiUtils
{
    [Serializable]
    internal class EdiUtilsException : Exception
    {
        public EdiUtilsException()
        {
        }

        public EdiUtilsException(string message) : base(message)
        {
        }

        public EdiUtilsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EdiUtilsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}