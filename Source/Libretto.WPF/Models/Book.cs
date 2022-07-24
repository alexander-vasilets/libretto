namespace Libretto.WPF.Models;

using System.Diagnostics.CodeAnalysis;

using CommunityToolkit.Mvvm.ComponentModel;

[ExcludeFromCodeCoverage]
public class Book : ObservableObject
{
    private string title = string.Empty;
    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    private string authorName = string.Empty;
    public string AuthorName
    {
        get => authorName;
        set => SetProperty(ref authorName, value);
    }
}