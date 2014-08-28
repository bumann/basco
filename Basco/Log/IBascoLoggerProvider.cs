namespace Basco.Log
{
    public interface IBascoLoggerProvider
    {
        IBascoLogger ActiveLogger { get; }
    }
}