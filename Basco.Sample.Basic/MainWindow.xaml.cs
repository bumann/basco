namespace Basco.Sample.Basic
{
    using System.Windows;
    using Basco.Sample.Basic.Fsm;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var statesFactory = new BascoStatesFactory();
            var bascoConfigurator = new BascoConfigurator();

            var driverViewModel1 = new DriverControlModel(BascoFactory.Create(statesFactory, bascoConfigurator));
            var driverViewModel2 = new DriverControlModel(BascoFactory.Create(statesFactory, bascoConfigurator));
            var driverViewModel3 = new DriverControlModel(BascoFactory.Create(statesFactory, bascoConfigurator));
            var driverViewModel4 = new DriverControlModel(BascoFactory.Create(statesFactory, bascoConfigurator));

            this.InitializeComponent();

            this.DriverOne.DataContext = driverViewModel1;
            this.DriverTwo.DataContext = driverViewModel2;
            this.DriverThree.DataContext = driverViewModel3;
            this.DriverFour.DataContext = driverViewModel4;
        }
    }
}
