namespace Basco.Sample.CompositeStates.Driver.States
{
    using Basco.Configuration;

    public class CompositeStateMachineConfigurator : IBascoConfigurator<TransitionTrigger>
    {
        public void Configurate(IBasco<TransitionTrigger> basco)
        {
            basco
                .In<StateA, TransitionTrigger>(x => x
                    .When(TransitionTrigger.Run).Goto<StateB>())
                .In<StateB, TransitionTrigger>(x => x
                    .When(TransitionTrigger.Stop).Goto<StateA>()
                    .When(TransitionTrigger.Error).Goto<StateC>())
                .In<StateC, TransitionTrigger>(x => x
                    .When(TransitionTrigger.Reset).Goto<StateB>());
        }
    }
}