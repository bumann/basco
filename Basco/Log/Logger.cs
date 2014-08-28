namespace Basco.Log
{
    using System;

    public class Logger : ILogger
    {
        public Logger()
        {
            Instance = this;
        }

        private static Logger Instance { get; set; }

        public static void LogDebug(string format, params object[] args)
        {
            if (Instance == null)
            {
                return;
            }

            Instance.Debug(format, args);
        }

        public static void LogError(Exception exception, string format, params object[] args)
        {
            if (Instance == null)
            {
                return;
            }

            Instance.Error(exception, format, args);
        }

        public virtual void Debug(string format, params object[] args)
        {
        }

        public virtual void Error(Exception exception, string format, params object[] args)
        {
        }
    }
}