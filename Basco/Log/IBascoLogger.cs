namespace Basco.Log
{
    using System;

    public interface IBascoLogger
    {
        void LogDebug(string format, params object[] args);

        void LogError(Exception exception, string format, params object[] args);
    }
}