namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Start(IState<TTransitionTrigger> initialState);

        void Stop();

        void Trigger(TTransitionTrigger trigger);
    }
}