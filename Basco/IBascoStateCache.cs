namespace Basco
{
    using System;

    public interface IBascoStateCache
    {
        void Initialize();

        IState GetState(Type stateType);
    }
}