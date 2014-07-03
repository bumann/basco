namespace Basco
{
    using System;

    /// <summary>
    /// A concrete implementation of this interface is needed to properly configure Basco FSM.
    /// </summary>
    /// <typeparam name="TTransitionTrigger"></typeparam>
    public interface IBascoConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Configurate(IBasco<TTransitionTrigger> basco);
    }
}