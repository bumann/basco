namespace Basco
{

    ////public interface ICompositeStateConfigurator<TTransitionTrigger>
    ////    where TTransitionTrigger : IComparable
    ////{
    ////    IBasco<TTransitionTrigger> WithSubStates(object obj);

    ////    ICompositeStateConfigurator<TTransitionTrigger> In<TState, TTransitionTrigger>(Action<IStateTransitionsBuilder<TTransitionTrigger>> builder)
    ////        where TTransitionTrigger : IComparable
    ////        where TState : class, IState;
    ////}

    ////internal class CompositeStateConfigurator<TTransitionTrigger> : ICompositeStateConfigurator<TTransitionTrigger>
    ////    where TTransitionTrigger : IComparable
    ////{
    ////    private readonly IBasco<TTransitionTrigger> basco;

    ////    public CompositeStateConfigurator(IBasco<TTransitionTrigger> basco)
    ////    {
    ////        this.basco = basco;
    ////    }

    ////    public IBasco<TTransitionTrigger> WithSubStates(object obj)
    ////    {
    ////        return this.basco;
    ////    }

    ////    ICompositeStateConfigurator<TTransitionTrigger1> ICompositeStateConfigurator<TTransitionTrigger>.In<TState, TTransitionTrigger1>(Action<IStateTransitionsBuilder<TTransitionTrigger1>> builder)
    ////    {
    ////        throw new NotImplementedException();
    ////    }

    ////    //public ICompositeStateConfigurator In<TState, TTransitionTrigger>(Action<IStateTransitionsBuilder<TTransitionTrigger>> builder)
    ////    //    where TState : class, IState
    ////    //    where TTransitionTrigger : IComparable
    ////    //{
    ////    //    return this;
    ////    //}
    ////}
}