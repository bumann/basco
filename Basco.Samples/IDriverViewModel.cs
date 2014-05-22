﻿namespace Basco.Samples
{
    using System.Windows;
    using System.Windows.Input;

    public interface IDriverViewModel
    {
        Visibility ConnectedVisibility { get; set; }

        Visibility ProcessingVisibility { get; set; }

        Visibility ErrorVisibility { get; set; }

        ICommand ConnectCommand { get; }

        ICommand ProcessCommand { get; }

        ICommand ErrorCommand { get; }

        ICommand ResetCommand { get; }

        ICommand DisconnectCommand { get; }
    }
}