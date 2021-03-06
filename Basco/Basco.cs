﻿namespace Basco
{
    using System;
    using System.Collections.Generic;
    using Basco.Configuration;
    using Basco.Execution;
    using Scyano;
    using Scyano.MessageProcessing;

    public class Basco<TTransitionTrigger> : IBascoInternal<TTransitionTrigger>, IMessageProcessor<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IScyano<TTransitionTrigger> scyano;
        private readonly IBascoConfigurator<TTransitionTrigger> bascoConfigurator;
        private readonly IList<IBascoInternal<TTransitionTrigger>> fsmList;

        public Basco(
            IScyano<TTransitionTrigger> scyano,
            IBascoStateCache<TTransitionTrigger> statesCache,
            IBascoTransitionCache<TTransitionTrigger> transitionCache,
            IBascoExecutor<TTransitionTrigger> bascoExecutor,
            IBascoConfigurator<TTransitionTrigger> bascoConfigurator = null)
        {
            this.scyano = scyano;
            this.StatesCache = statesCache;
            this.TransitionCache = transitionCache;
            this.BascoExecutor = bascoExecutor;
            this.bascoConfigurator = bascoConfigurator;
            this.fsmList = new List<IBascoInternal<TTransitionTrigger>>();
        }

        public event EventHandler StateChanged;

        public bool IsInitialized { get; private set; }

        public bool IsRunning { get; private set; }

        public IState CurrentState
        {
            get { return this.BascoExecutor.CurrentState; }
        }

        public IBascoStateCache<TTransitionTrigger> StatesCache { get; private set; }

        public IBascoTransitionCache<TTransitionTrigger> TransitionCache { get; private set; }

        public IBascoExecutor<TTransitionTrigger> BascoExecutor { get; private set; }

        public void AddTransitionCache(Type type, StateTransitions<TTransitionTrigger> stateTransitions) 
        {
            this.TransitionCache.Add(type, stateTransitions);
        }

        public void AddHierchary(IBascoInternal<TTransitionTrigger> subBasco)
        {
            this.fsmList.Add(subBasco);
            subBasco.StateChanged += this.HandleSubStateChanged;
        }

        public void Initialize(IEnumerable<IState> states, Type startStateType)
        {
            this.scyano.Initialize(this);
            this.StatesCache.Initialize(this, states);

            //// TODO
            this.BascoExecutor.StateChanged += this.OnStateChanged;

            if (this.bascoConfigurator != null)
            {
                this.bascoConfigurator.Configurate(this);
            }

            this.BascoExecutor.Initialize(startStateType);
            this.IsInitialized = true;
        }

        public void Start()
        {
            if (!this.IsInitialized)
            {
                throw new BascoException("The state machine was not initialized. Do call Basco.Initialize()!");
            }

            if (this.IsRunning)
            {
                throw new BascoException("The state machine was already started. Do not call Basco.Start() twice!");
            }

            this.IsRunning = true;
            try
            {
                this.scyano.Start();
                this.BascoExecutor.Start();
            }
            catch (Exception)
            {
                this.scyano.Stop();
                this.IsRunning = false;
                throw;
            }
        }

        public void Stop()
        {
            this.IsRunning = false;
            this.BascoExecutor.Stop();
            this.scyano.Stop();
        }

        public IState RetrieveState<TState>() where TState : class, IState
        {
            return this.BascoExecutor.RetrieveState<TState>();
        }

        public void Trigger(TTransitionTrigger trigger)
        {
            this.scyano.Enqueue(trigger);
        }

        public void ProcessMessage(TTransitionTrigger message)
        {
            foreach (var basco in this.fsmList)
            {
                basco.BascoExecutor.ChangeState(message);
            }

            this.BascoExecutor.ChangeState(message);
        }

        private void HandleSubStateChanged(object sender, EventArgs e)
        {
            this.OnStateChanged(sender, e);
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }
    }
}