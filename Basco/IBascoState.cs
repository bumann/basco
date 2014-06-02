namespace Basco
{
    using System;

    public interface IBascoState
    {
        event EventHandler StateChanged;

        IState CurrentState { get; }
    }
}