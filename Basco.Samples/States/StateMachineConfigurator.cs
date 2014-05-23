﻿namespace Basco.Samples.States
{
    using System.Collections.Generic;

    public class StateMachineConfigurator : IBascoConfigurator<Transitions>
    {
        private readonly IConnectedState connectedState;
        private readonly IProcessingState processingState;
        private readonly IErrorState errorState;

        public StateMachineConfigurator(IConnectedState connectedState, IProcessingState processingState, IErrorState errorState)
        {
            this.connectedState = connectedState;
            this.processingState = processingState;
            this.errorState = errorState;
        }

        public IEnumerable<IState<Transitions>> Configurate()
        {
            this.connectedState.On(Transitions.Run).Goto(this.processingState)
                .On(Transitions.Stop).ReEnter()
                .On(Transitions.Error).Stay()
                .On(Transitions.Reset).Stay();

            this.processingState
                .On(Transitions.Stop).Goto(this.connectedState)
                .On(Transitions.Error).Goto(this.errorState)
                .On(Transitions.Reset).Stay();

            this.errorState
                .On(Transitions.Reset).Goto(this.connectedState);

            return new List<IState<Transitions>>
            {
                this.connectedState,
                this.processingState,
                this.errorState
            };
        }
    }
}