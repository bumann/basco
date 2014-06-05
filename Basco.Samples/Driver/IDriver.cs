namespace Basco.Samples.Driver
{
    using Basco.Samples.Driver.States;

    public interface IDriver
    {
        IBasco<TransitionTrigger> Basco { get; }
    }
}