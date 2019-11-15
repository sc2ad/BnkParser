using System;
using System.Runtime.Serialization;

namespace CustomSongsLoader
{
    [Serializable]
    public class AssetsException : Exception
    {
        public AssetsException()
        {
        }

        public AssetsException(string message) : base(message)
        {
        }

        public AssetsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AssetsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}