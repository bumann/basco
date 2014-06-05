namespace Basco.NinjectExtensions
{
    using Appccelerate.AsyncModule;
    using Ninject.Syntax;

    public class BascoConfigurator : IBascoConfigurator
    {
        private readonly IBindingRoot bindingRoot;

        public BascoConfigurator(IBindingRoot bindingRoot)
        {
            this.bindingRoot = bindingRoot;

            this.bindingRoot.Bind(typeof(IBasco<>)).To(typeof(Basco<>));
            this.bindingRoot.Bind(typeof(IStateTransitions<>)).To(typeof(StateTransitions<>));
        }

        public string ScopeName { get; set; }

        public IBascoConfigurator WithConfigurator<TBascoConfigurator>()
        {
            this.bindingRoot.Bind(typeof(IBascoConfigurator<>)).To<TBascoConfigurator>();
            return this;
        }

        public IBascoConfigurator WithModuleController()
        {
            this.bindingRoot.Bind<IModuleController>().To<ModuleController>();
            return this;
        }

        public IBascoConfigurator InNamedScope(string scopeName)
        {
            this.ScopeName = scopeName;
            return this;
        }
    }
}