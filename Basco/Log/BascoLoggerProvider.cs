namespace Basco.Log
{
    public class BascoLoggerProvider : IBascoLoggerProvider
    {
        public BascoLoggerProvider(IBascoLogger logger)
        {
            this.ActiveLogger = logger ?? new DummyLogger();
        }

        public IBascoLogger ActiveLogger { get; private set; }
    }
}