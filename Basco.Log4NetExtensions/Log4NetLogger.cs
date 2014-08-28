namespace Basco.Log4NetExtensions
{
    using System;
    using System.Globalization;
    using Basco.Log;
    using log4net;

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Log4NetLogger : IBascoLogger
    {
        private static int count;
        private readonly ILog logger;

        public Log4NetLogger()
        {
            count++;
            this.logger = LogManager.GetLogger(string.Format("Basco [{0}]:", count));
        }

        public void LogDebug(string format, params object[] args)
        {
            this.logger.DebugFormat(format, args);
        }

        public void LogError(Exception exception, string format, params object[] args)
        {
            this.logger.Error(string.Format(CultureInfo.InvariantCulture, format, args), exception);
        }
    }
}