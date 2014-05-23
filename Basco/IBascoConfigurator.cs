namespace Basco
{
    using System;
    using System.Collections.Generic;

    public interface IBascoConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IEnumerable<IState<TTransitionTrigger>> Configurate();
    }
}