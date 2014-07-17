namespace Basco.Sample.CompositeStates.Driver.States
{
    public interface IDriver
    {
        IBasco<TransitionTrigger> Basco { get; }

        IStateB StateB { get; }
    }
}