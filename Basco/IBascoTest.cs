namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger> : IBascoController<TTransitionTrigger>, IBascoState<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
    }
}