namespace Basco
{
    using System;

    public interface IBascoStateCache
    {
        void Initialize();

        IState RetrieveState(Type stateType);
    }
}