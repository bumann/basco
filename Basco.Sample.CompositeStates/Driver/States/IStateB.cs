namespace Basco.Sample.CompositeStates.Driver.States
{
    using System;

    public interface IStateB : IStateEnter, IStateExit, IStateUsingBasco<TransitionTrigger>
    {
        event EventHandler ProcessingChanged;

        int ItemCount { get; }
    }
}