namespace Basco.Sample.CompositeStates
{
    using System.Windows;
    using System.Windows.Input;

    public interface IDriverControlModel
    {
        Visibility ConnectedVisibility { get; set; }

        Visibility ProcessingVisibility { get; set; }

        Visibility ErrorVisibility { get; set; }

        ICommand StartCommand { get; }

        ICommand RunCommand { get; }

        ICommand ErrorCommand { get; }

        ICommand ResetCommand { get; }

        ICommand StopCommand { get; }
    }
}