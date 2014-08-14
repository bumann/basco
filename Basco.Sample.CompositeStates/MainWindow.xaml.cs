namespace Basco.Sample.CompositeStates
{
    using System.Reflection;
    using Ninject;

    public partial class MainWindow
    {
        public MainWindow()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var driverViewModel1 = kernel.Get<IDriverControlModel>();
            var driverViewModel2 = kernel.Get<IDriverControlModel>();
            var driverViewModel3 = kernel.Get<IDriverControlModel>();
            var driverViewModel4 = kernel.Get<IDriverControlModel>();

            this.InitializeComponent();

            this.DriverOne.DataContext = driverViewModel1;
            this.DriverTwo.DataContext = driverViewModel2;
            this.DriverThree.DataContext = driverViewModel3;
            this.DriverFour.DataContext = driverViewModel4;
        }
    }
}
