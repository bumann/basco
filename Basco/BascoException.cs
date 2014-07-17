namespace Basco
{
    using System;

    public class BascoException : Exception
    {
        public BascoException(string message)
            : base(message)
        {
        }

        public BascoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}