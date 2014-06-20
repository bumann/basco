namespace Basco.NinjectExtensions
{
    public interface IBascoConfigurator
    {
        IBascoConfigurator WithConfigurator<TBascoConfigurator>();

        IBascoConfigurator WithScyano();

        IBascoConfigurator InNamedScope(string scopeName);
    }
}