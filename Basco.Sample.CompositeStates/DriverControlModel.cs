﻿namespace Basco.Sample.CompositeStates
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Basco.Sample.CompositeStates.Driver;
    using Basco.Sample.CompositeStates.Driver.States;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class DriverControlModel : IDriverControlModel
    {
        private readonly IDriver driver;
        private readonly IStateB stateB;

        private ICommand startCommand;
        private ICommand runCommand;
        private ICommand pauseCommand;
        private ICommand errorCommand;
        private ICommand resetCommand;
        private ICommand stopCommand;

        public DriverControlModel(IDriver driver)
        {
            this.driver = driver;
            this.driver.Basco.StateChanged += this.OnDriverStateChanged;

            this.stateB = this.driver.StateB;
            this.stateB.ProcessingChanged += this.OnStateBChanged;

            this.OnDriverStateChanged(null, new EventArgs());
            this.DisplayInfo = "Basco Demo";
        }

        public string DisplayInfo { get; set; }

        public bool CanStart { get; set; }

        public bool CanStop { get; set; }

        public Visibility ConnectedVisibility { get; set; }

        public Visibility ConnectedSubDVisibility { get; set; }

        public Visibility ConnectedSubEVisibility { get; set; }

        public Visibility ProcessingVisibility { get; set; }

        public Visibility ProcessingSubFVisibility { get; set; }

        public Visibility ProcessingSubGVisibility { get; set; }

        public Visibility ErrorVisibility { get; set; }

        public ICommand StartCommand
        {
            get { return this.startCommand ?? (this.startCommand = new RelayCommand(param => this.Start())); }
        }

        public ICommand RunCommand
        {
            get { return this.runCommand ?? (this.runCommand = new RelayCommand(param => this.Run())); }
        }

        public ICommand PauseCommand
        {
            get { return this.pauseCommand ?? (this.pauseCommand = new RelayCommand(param => this.Pause())); }
        }

        public ICommand ErrorCommand
        {
            get { return this.errorCommand ?? (this.errorCommand = new RelayCommand(param => this.Error())); }
        }

        public ICommand ResetCommand
        {
            get { return this.resetCommand ?? (this.resetCommand = new RelayCommand(param => this.Reset())); }
        }

        public ICommand StopCommand
        {
            get { return this.stopCommand ?? (this.stopCommand = new RelayCommand(param => this.Stop())); }
        }

        public void Start()
        {
            this.driver.Basco.Start();
        }

        public void Run()
        {
            this.driver.Basco.Trigger(TransitionTrigger.Run);
        }

        public void Pause()
        {
            this.driver.Basco.Trigger(TransitionTrigger.Pause);
        }

        public void Error()
        {
            this.driver.Basco.Trigger(TransitionTrigger.Error);
        }

        public void Reset()
        {
            this.driver.Basco.Trigger(TransitionTrigger.Reset);
        }

        public void Stop()
        {
            this.driver.Basco.Stop();
        }

        private void OnDriverStateChanged(object sender, EventArgs eventArgs)
        {
            this.CanStart = !this.driver.Basco.IsRunning;
            this.CanStop = this.driver.Basco.IsRunning;

            this.ConnectedVisibility = this.driver.Basco.IsRunning && this.driver.Basco.CurrentState is IStateA ? Visibility.Visible : Visibility.Hidden;
            this.ConnectedSubDVisibility = Visibility.Hidden;
            this.ConnectedSubEVisibility = Visibility.Hidden;

            var composite = this.driver.Basco.CurrentState as IBascoCompositeState<TransitionTrigger>;
            if (composite != null)
            {
                this.ProcessingSubFVisibility = composite.Basco.CurrentState is SubStateF ? Visibility.Visible : Visibility.Hidden;
                this.ProcessingSubGVisibility = composite.Basco.CurrentState is SubStateG ? Visibility.Visible : Visibility.Hidden;
                if (this.ProcessingSubFVisibility == Visibility.Visible || this.ProcessingSubGVisibility == Visibility.Visible)
                {
                    this.ProcessingVisibility = Visibility.Visible;
                }
            }
            else
            {
                this.HideProcessingState();
            }

            this.ErrorVisibility = this.driver.Basco.CurrentState is IStateC ? Visibility.Visible : Visibility.Hidden;
        }

        private void OnStateBChanged(object sender, EventArgs eventArgs)
        {
            this.DisplayInfo = "Items: " + this.stateB.ItemCount;
        }

        private void HideProcessingState()
        {
            this.ProcessingVisibility = Visibility.Hidden;
            this.ProcessingSubFVisibility = Visibility.Hidden;
            this.ProcessingSubGVisibility = Visibility.Hidden;
        }
    }
}