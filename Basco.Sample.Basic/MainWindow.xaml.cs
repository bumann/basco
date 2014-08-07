namespace Basco.Sample.Basic
{
    using System.Collections.Generic;
    using System.Windows;
    using Basco.Sample.Basic.Fsm;
    using Basco.Sample.Basic.Fsm.States;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var states = new List<IState> { new ConnectedState(), new ProcessingState(), new ErrorState() };
            var bascoConfigurator = new BascoConfigurator();

            var driverViewModel1 = new DriverControlModel(BascoFactory.Create(states, bascoConfigurator));
            var driverViewModel2 = new DriverControlModel(BascoFactory.Create(states, bascoConfigurator));
            var driverViewModel3 = new DriverControlModel(BascoFactory.Create(states, bascoConfigurator));
            var driverViewModel4 = new DriverControlModel(BascoFactory.Create(states, bascoConfigurator));

            this.InitializeComponent();

            this.DriverOne.DataContext = driverViewModel1;
            this.DriverTwo.DataContext = driverViewModel2;
            this.DriverThree.DataContext = driverViewModel3;
            this.DriverFour.DataContext = driverViewModel4;
        }
    }
}
