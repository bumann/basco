namespace Basco.Sample.UsingNinject.Driver.States
{
    using System;
    using Basco;

    public interface IProcessingState : IStateExit, IStateUsingBasco<TransitionTrigger>
    {
        event EventHandler ProcessingChanged;

        int ItemCount { get; }
    }
}