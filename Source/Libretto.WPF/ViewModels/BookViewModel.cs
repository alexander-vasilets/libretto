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

    private Book? selectedBook = null;

    private RelayCommand<Book>? selectUnselectBookCommand;
    public RelayCommand<Book>? SelectUnselectBookCommand
    {
        get => selectUnselectBookCommand;
        set => SetProperty(ref selectUnselectBookCommand, value);
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

        SelectUnselectBookCommand = new(book => SelectUnselectBook(book!));
        DeleteBookCommand = new(bookId => collection.Delete(bookId));
        AddBookCommand = new(AddBook);
        UpdateBookCommand = new(UpdateBook);
    }

    private void SelectUnselectBook(Book clickedBook)
    {
        // No book is selected
        if (selectedBook == null)
        {
            selectedBook = clickedBook;
            selectedBook.IsSelected = true;
            CurrentBook = new(selectedBook);
            return;
        }
        // A book is selected and it is this one
        if (selectedBook.Id == clickedBook.Id)
        {
            selectedBook.IsSelected = false;
            selectedBook = null;
            CurrentBook = new();
            return;
        }
        // A book is selected but it is another book
        selectedBook.IsSelected = false;
        selectedBook = clickedBook;
        selectedBook.IsSelected = true;
        CurrentBook = new(clickedBook);
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
