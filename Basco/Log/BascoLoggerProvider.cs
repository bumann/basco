namespace Basco.Log
{
    using System.Collections.Generic;
    using System.Linq;

    public class BascoLoggerProvider : IBascoLoggerProvider
    {
        private readonly IEnumerable<IBascoLogger> loggers;

        public BascoLoggerProvider(IEnumerable<IBascoLogger> loggers)
        {
            this.loggers = loggers.ToList();

            this.ActiveLogger = this.loggers.FirstOrDefault();
            
            // TODO: more sophisticated choice
            if (this.loggers.Count() > 1)
            {
                this.ActiveLogger = this.loggers.First(x => !(x is DummyLogger));
            }
        }

        public IBascoLogger ActiveLogger { get; private set; }
    }
}