namespace Basco.Samples
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Basco.Samples.States;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class DriverViewModel : IDriverViewModel
    {
        private readonly IDriver driver;
        private readonly IBascoExecutor<Transitions> bascoExecutor;
        private readonly IProcessingState processingState;

        private ICommand connectCommand;
        private ICommand processCommand;
        private ICommand errorCommand;
        private ICommand resetCommand;
        private ICommand disconnectCommand;

        public DriverViewModel(
            IDriver driver,
            IBascoExecutor<Transitions> bascoExecutor,
            IProcessingState processingState)
        {
            this.driver = driver;
            this.bascoExecutor = bascoExecutor;
            this.processingState = processingState;

            this.bascoExecutor.StateChanged += this.OnDriverStateChanged;
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
            this.driver.Connect();
        }

        public void Run()
        {
            this.driver.RunProgram();
        }

        public void Error()
        {
            this.driver.ProduceError();
        }

        public void Reset()
        {
            this.driver.ResetError();
        }

        public void Disconnect()
        {
            this.driver.Disconnect();
        }

        private void OnDriverStateChanged(object sender, EventArgs eventArgs)
        {
            this.ConnectedVisibility = this.bascoExecutor.CurrentState is IConnectedState ? Visibility.Visible : Visibility.Hidden;
            this.ProcessingVisibility = this.bascoExecutor.CurrentState is IProcessingState ? Visibility.Visible : Visibility.Hidden;
            this.ErrorVisibility = this.bascoExecutor.CurrentState is IErrorState ? Visibility.Visible : Visibility.Hidden;
        }

        private void OnProcessingStateChanged(object sender, EventArgs eventArgs)
        {
            this.DisplayInfo = "Items: " + this.processingState.ItemCount;
        }
    }
}