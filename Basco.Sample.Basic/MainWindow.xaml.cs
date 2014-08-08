namespace Basco.Sample.Basic
{
    using Basco.Sample.Basic.Fsm;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            var driverViewModel1 = new DriverControlModel(BascoFactory.Create(new BascoConfigurator()));
            var driverViewModel2 = new DriverControlModel(BascoFactory.Create(new BascoConfigurator()));
            var driverViewModel3 = new DriverControlModel(BascoFactory.Create(new BascoConfigurator()));
            var driverViewModel4 = new DriverControlModel(BascoFactory.Create(new BascoConfigurator()));

            this.InitializeComponent();

            this.DriverOne.DataContext = driverViewModel1;
            this.DriverTwo.DataContext = driverViewModel2;
            this.DriverThree.DataContext = driverViewModel3;
            this.DriverFour.DataContext = driverViewModel4;
        }
    }
}
