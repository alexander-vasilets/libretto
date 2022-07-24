namespace Libretto.WPF.Views;

using System.Diagnostics.CodeAnalysis;
using System.Windows;

using ViewModels;

[ExcludeFromCodeCoverage]
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }
}
