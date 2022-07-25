namespace Libretto.WPF.Views;

using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using ViewModels;

[ExcludeFromCodeCoverage]
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = App.Current.Services.GetService<BookViewModel>();
    }
}
