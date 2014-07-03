namespace Basco
{
    using System;

    public interface IBascoState
    {
        event EventHandler StateChanged;

        IState CurrentState { get; }

        IState RetrieveState<TState>() where TState : class, IState;
    }
}