namespace Libretto.UnitTests.ViewModels;

using FluentAssertions;

using WPF.ViewModels;

public class MainViewModelTests
{
    [Fact]
    public void OnCreation_ShouldSeedData()
    {
        // Arrange

        // Act
        var viewModel = new MainViewModel();

        // Assert
        viewModel.Books.Should().NotBeEmpty("Books collection should be seeded on initialization!");
    }
}
