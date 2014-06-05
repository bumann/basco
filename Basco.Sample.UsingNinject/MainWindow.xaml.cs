using System.Reflection;
using System.Windows;
using Ninject;

namespace Basco.Sample.UsingNinject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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