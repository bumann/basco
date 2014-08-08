namespace Basco.Sample.CompositeStates.Driver.States
{
    public interface ISubStateD : IStateUsingBasco<TransitionTrigger>
    {
        bool Active { get; }
    }
}