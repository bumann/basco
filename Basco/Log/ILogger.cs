namespace Basco.Log
{
    using System;

    public interface ILogger
    {
        void Debug(string format, params object[] args);

        void Error(Exception exception, string format, params object[] args);
    }
}