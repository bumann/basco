namespace Basco.Sample.Basic
{
    using System.Collections.Generic;
    using System.Windows;
    using Basco.Async;
    using Basco.Sample.Basic.Fsm;
    using Basco.Sample.Basic.Fsm.States;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var driverViewModel1 = new DriverControlModel(new Basco<TransitionTrigger>(new Scyano(), new BascoConfigurator(), new BascoExecutor<TransitionTrigger>(new List<IState> { new ConnectedState(), new ProcessingState(), new ErrorState() })));
            var driverViewModel2 = new DriverControlModel(new Basco<TransitionTrigger>(new Scyano(), new BascoConfigurator(), new BascoExecutor<TransitionTrigger>(new List<IState> { new ConnectedState(), new ProcessingState(), new ErrorState() })));
            var driverViewModel3 = new DriverControlModel(new Basco<TransitionTrigger>(new Scyano(), new BascoConfigurator(), new BascoExecutor<TransitionTrigger>(new List<IState> { new ConnectedState(), new ProcessingState(), new ErrorState() })));
            var driverViewModel4 = new DriverControlModel(new Basco<TransitionTrigger>(new Scyano(), new BascoConfigurator(), new BascoExecutor<TransitionTrigger>(new List<IState> { new ConnectedState(), new ProcessingState(), new ErrorState() })));

            this.InitializeComponent();

            this.DriverOne.DataContext = driverViewModel1;
            this.DriverTwo.DataContext = driverViewModel2;
            this.DriverThree.DataContext = driverViewModel3;
            this.DriverFour.DataContext = driverViewModel4;
        }
    }
}
