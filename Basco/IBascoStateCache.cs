namespace Basco
{
    using System;

    public interface IBascoStateCache
    {
        IState GetState(Type stateType);

        void Initialize();
    }
}