namespace Basco
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class BascoException : Exception
    {
        public BascoException()
        {
        }

        public BascoException(string message)
            : base(message)
        {
        }

        public BascoException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected BascoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}