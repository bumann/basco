namespace Basco.Sample.Basic
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Basco.Sample.Basic.Fsm.States;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class DriverControlModel : IDriverControlModel
    {
        private readonly IBasco<TransitionTrigger> basco;
        private readonly IProcessingState processingState;

        private ICommand connectCommand;
        private ICommand processCommand;
        private ICommand errorCommand;
        private ICommand resetCommand;
        private ICommand disconnectCommand;

        public DriverControlModel(IBasco<TransitionTrigger> basco)
        {
            this.basco = basco;
            this.basco.InitializeWithStartState<ConnectedState>();
            this.basco.StateChanged += this.OnDriverStateChanged;

            this.processingState = (IProcessingState)this.basco.RetrieveState<ProcessingState>();
            this.processingState.ProcessingChanged += this.OnProcessingStateChanged;

            this.OnDriverStateChanged(null, new EventArgs());
            this.DisplayInfo = "Basco Demo";
        }

        public string DisplayInfo { get; set; }

        public Visibility ConnectedVisibility { get; set; }

        public Visibility ProcessingVisibility { get; set; }

        public Visibility ErrorVisibility { get; set; }

        public ICommand ConnectCommand
        {
            get { return this.connectCommand ?? (this.connectCommand = new RelayCommand(param => this.Connect())); }
        }

        public ICommand ProcessCommand
        {
            get { return this.processCommand ?? (this.processCommand = new RelayCommand(param => this.Run())); }
        }

        public ICommand ErrorCommand
        {
            get { return this.errorCommand ?? (this.errorCommand = new RelayCommand(param => this.Error())); }
        }

        public ICommand ResetCommand
        {
            get { return this.resetCommand ?? (this.resetCommand = new RelayCommand(param => this.Reset())); }
        }

        public ICommand DisconnectCommand
        {
            get { return this.disconnectCommand ?? (this.disconnectCommand = new RelayCommand(param => this.Disconnect())); }
        }

        public void Connect()
        {
            this.basco.Start();
        }

        public void Run()
        {
            this.basco.Trigger(TransitionTrigger.Run);
        }

        public void Error()
        {
            this.basco.Trigger(TransitionTrigger.Error);
        }

        public void Reset()
        {
            this.basco.Trigger(TransitionTrigger.Reset);
        }

        public void Disconnect()
        {
            this.basco.Stop();
        }

        private void OnDriverStateChanged(object sender, EventArgs eventArgs)
        {
            this.ConnectedVisibility = this.basco.CurrentState is IConnectedState ? Visibility.Visible : Visibility.Hidden;
            this.ProcessingVisibility = this.basco.CurrentState is IProcessingState ? Visibility.Visible : Visibility.Hidden;
            this.ErrorVisibility = this.basco.CurrentState is IErrorState ? Visibility.Visible : Visibility.Hidden;
        }

        private void OnProcessingStateChanged(object sender, EventArgs eventArgs)
        {
            this.DisplayInfo = "Items: " + this.processingState.ItemCount;
        }
    }
}