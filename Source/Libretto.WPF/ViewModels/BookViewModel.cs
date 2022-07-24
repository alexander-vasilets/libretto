namespace Libretto.WPF.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Models;
using Services;

[ExcludeFromCodeCoverage]
public class BookViewModel : ObservableObject
{
    private readonly IBookCollection collection;
    public ObservableCollection<Book> Books => collection.Books;


    private RelayCommand<Guid>? deleteBookCommand;
    public RelayCommand<Guid>? DeleteBookCommand
    {
        get => deleteBookCommand;
        set => SetProperty(ref deleteBookCommand, value);
    }

    public BookViewModel(IBookCollection bookCollection)
    {
        collection = bookCollection;
        collection.SeedData();

        DeleteBookCommand = new(bookId => collection.Delete(bookId));
    }
}
