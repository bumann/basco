using System.Windows;

namespace Basco.Sample.CompositeStates
{
    using System.Reflection;
    using Ninject;

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

            this.InitializeComponent();

            this.DriverOne.DataContext = driverViewModel1;
            this.DriverTwo.DataContext = driverViewModel2;
        }
    }
}
