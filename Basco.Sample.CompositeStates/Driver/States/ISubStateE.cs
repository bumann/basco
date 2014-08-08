namespace Basco.Sample.CompositeStates.Driver.States
{
    public interface ISubStateE : IStateUsingBasco<TransitionTrigger>
    {
        bool Active { get; }
    }
}