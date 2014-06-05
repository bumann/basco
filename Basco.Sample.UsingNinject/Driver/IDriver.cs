namespace Basco.Sample.UsingNinject.Driver
{
    using Basco;
    using Basco.Sample.UsingNinject.Driver.States;

    public interface IDriver
    {
        IBasco<TransitionTrigger> Basco { get; }

        IProcessingState ProcessingState { get; }
    }
}