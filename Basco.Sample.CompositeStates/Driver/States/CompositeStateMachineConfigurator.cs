namespace Basco.Sample.CompositeStates.Driver.States
{
    using Basco.Configuration;

    public class CompositeStateMachineConfigurator : IBascoConfigurator<TransitionTrigger>
    {
        public void Configurate(IBascoInternal<TransitionTrigger> basco)
        {
            basco.For<StateA, TransitionTrigger>(s => s
                .When(TransitionTrigger.Run).Goto<StateB>());

            basco.ForComposite<StateB, TransitionTrigger>(cs => cs
                .WithSubStates(b => b
                    .AlwaysStartInInitialState()
                    .For<SubStateF, TransitionTrigger>(state => state
                        .When(TransitionTrigger.ContinueG).Goto<SubStateG>())
                    .For<SubStateG, TransitionTrigger>(state => state
                        .When(TransitionTrigger.ContinueF).Goto<SubStateF>()))
                .When(TransitionTrigger.Pause).Goto<StateA>()
                .When(TransitionTrigger.Error).Goto<StateC>());

            basco.For<StateC, TransitionTrigger>(state => state
                .When(TransitionTrigger.Pause).Goto<StateA>()
                .When(TransitionTrigger.Reset).Goto<StateB>());
        }
    }
}