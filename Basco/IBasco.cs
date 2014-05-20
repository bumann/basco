namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        bool IsRunning { get; }

        void Start(IState<TTransitionTrigger> initialState);

        void Stop();

        void Trigger(TTransitionTrigger trigger);
    }
}