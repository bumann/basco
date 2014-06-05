namespace Basco.Sample.UsingNinject.Driver.States
{
    using System;
    using Basco;

    public interface IProcessingState : IState, IStateExit
    {
        event EventHandler ProcessingChanged;

        int ItemCount { get; }
    }
}