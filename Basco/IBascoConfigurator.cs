namespace Basco
{
    using System;

    public interface IBascoConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Configurate(IBasco<TTransitionTrigger> basco);
    }
}