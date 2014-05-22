namespace Basco.Samples
{
    using System.Reflection;
    using System.Windows;
    using Ninject;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDriverViewModel driverViewModel;

        public MainWindow()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            this.driverViewModel = kernel.Get<IDriverViewModel>();
            this.DataContext = this.driverViewModel;

            this.InitializeComponent();
        }
    }
}
