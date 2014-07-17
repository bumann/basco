namespace Basco.Sample.CompositeStates.Driver.States
{
    using System;

    public interface IStateB : IStateEnter, IStateExit
    {
        event EventHandler ProcessingChanged;

        int ItemCount { get; }
    }
}