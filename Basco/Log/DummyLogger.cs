namespace Basco.Log
{
    using System;

    public class DummyLogger : IBascoLogger
    {
        public void LogDebug(string format, params object[] args)
        {
        }

        public void LogError(Exception exception, string format, params object[] args)
        {
        }
    }
}