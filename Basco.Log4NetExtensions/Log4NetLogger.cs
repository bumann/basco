namespace Basco.Log4NetExtensions
{
    using System;
    using System.Globalization;
    using Basco.Log;
    using log4net;

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Log4NetLogger : IBascoLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger("Basco");

        public void LogDebug(string format, params object[] args)
        {
            Logger.DebugFormat(format, args);
        }

        public void LogError(Exception exception, string format, params object[] args)
        {
            Logger.Error(string.Format(CultureInfo.InvariantCulture, format, args), exception);
        }
    }
}