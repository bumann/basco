namespace Basco.NinjectExtensions
{
    public interface IBascoConfigurator
    {
        IBascoConfigurator WithConfigurator<TBascoConfigurator>();

        IBascoConfigurator WithModuleController();

        IBascoConfigurator InNamedScope(string scopeName);
    }
}