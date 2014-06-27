namespace Basco.NinjectExtensions
{
    using Ninject.Syntax;
    using Scyano.NinjectExtensions;

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

        public IBascoConfigurator WithScyano()
        {
            this.bindingRoot.BindScyano();
            return this;
        }

        public IBascoConfigurator InNamedScope(string scopeName)
        {
            this.ScopeName = scopeName;
            return this;
        }
    }
}