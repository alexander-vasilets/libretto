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

    private Book currentBook = new();
    public Book CurrentBook
    {
        get => currentBook;
        private set => SetProperty(ref currentBook, value);
    }

    private bool bookSelected;
    public bool BookSelected
    {
        get => bookSelected;
        set => SetProperty(ref bookSelected, value);
    }

    private RelayCommand<Book>? selectBookCommand;
    public RelayCommand<Book>? SelectBookCommand
    {
        get => selectBookCommand;
        set => SetProperty(ref selectBookCommand, value);
    }

    private RelayCommand? unselectBookCommand;
    public RelayCommand? UnselectBookCommand
    {
        get => unselectBookCommand;
        set => SetProperty(ref unselectBookCommand, value);
    }

    private RelayCommand<Guid>? deleteBookCommand;
    public RelayCommand<Guid>? DeleteBookCommand
    {
        get => deleteBookCommand;
        set => SetProperty(ref deleteBookCommand, value);
    }

    private RelayCommand? addBookCommand;
    public RelayCommand? AddBookCommand
    {
        get => addBookCommand;
        set => SetProperty(ref addBookCommand, value);
    }

    private RelayCommand? updateBookCommand;
    public RelayCommand? UpdateBookCommand
    {
        get => updateBookCommand;
        set => SetProperty(ref updateBookCommand, value);
    }

    public BookViewModel(IBookCollection bookCollection)
    {
        collection = bookCollection;
        collection.SeedData();

        CurrentBook = new();

        SelectBookCommand = new(book => SelectBook(book!));
        UnselectBookCommand = new(UnselectBook);
        DeleteBookCommand = new(bookId => collection.Delete(bookId));
        AddBookCommand = new(AddBook);
        UpdateBookCommand = new(UpdateBook);
    }

    private void SelectBook(Book prototype)
    {
        CurrentBook = new Book(prototype!);
        BookSelected = true;
    }

    private void UnselectBook()
    {
        BookSelected = false;
        CurrentBook = new();
    }

    private void AddBook()
    {
        var newBook = new Book() { Title = CurrentBook.Title, AuthorName = CurrentBook.AuthorName };
        collection.Add(newBook);
        CurrentBook = new();
    }

    private void UpdateBook()
    {
        collection.Update(CurrentBook);
        CurrentBook = new();
    }
}
