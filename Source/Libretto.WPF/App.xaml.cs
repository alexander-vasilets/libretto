namespace Libretto.WPF;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Services;
using ViewModels;

[ExcludeFromCodeCoverage]
public partial class App : Application
{
    public readonly IServiceProvider Services;

    public new static App Current => (App)Application.Current;

    public App()
    {
        Services = ConfigureServices();
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IBookCollection, BookCollection>();
        services.AddSingleton<BookViewModel>();

        return services.BuildServiceProvider();
    }
}
