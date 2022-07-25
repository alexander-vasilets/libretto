namespace Libretto.WPF.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using CommunityToolkit.Mvvm.ComponentModel;

[ExcludeFromCodeCoverage]
public class Book : ObservableValidator
{
    private Guid id;
    public Guid Id => id;

    private string title;

    [Required(ErrorMessage = "Title is required!")]
    [MinLength(1, ErrorMessage = "Title should be at least 1 character long!")]
    [MaxLength(100, ErrorMessage = "Title should be not longer than 255 characters!")]
    public string Title
    {
        get => title;
        set
        {
            SetProperty(ref title, value);
            ValidateProperty(value);
        }
    }

    private string authorName;

    [Required(ErrorMessage = "Author name is required!")]
    [MinLength(1, ErrorMessage = "Author name should be at least 1 character long!")]
    [MaxLength(100, ErrorMessage = "Author name should be not longer than 255 characters!")]
    public string AuthorName
    {
        get => authorName;
        set
        {
            SetProperty(ref authorName, value);
            ValidateProperty(value);
        }
    }

    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set => SetProperty(ref isSelected, value);
    }

    public Book()
    {
        id = Guid.NewGuid();
        title = string.Empty;
        authorName = string.Empty;
        ValidateAllProperties();
    }

    public Book(Book prototype)
    {
        id = prototype.Id;
        title = prototype.Title;
        authorName = prototype.AuthorName;
        ValidateAllProperties();
    }
}