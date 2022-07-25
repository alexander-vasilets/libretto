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
        private set
        {
            if (SetProperty(ref currentBook, value))
            {
                SetProperty(ref canAdd, !value.HasErrors, nameof(CanAdd));
                SetProperty(ref canUpdate, selectedBook != null && canAdd, nameof(CanUpdate));
            }
        }
    }

    private Book? selectedBook = null;
    public Book? SelectedBook
    {
        get => selectedBook;
        private set
        {
            if (SetProperty(ref selectedBook, value))
            {
                SetProperty(ref canUpdate, value != null, nameof(CanUpdate));
            }
        }
    }

    private bool canUpdate;
    public bool CanUpdate => canUpdate;

    private bool canAdd;
    public bool CanAdd => canAdd;

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
        CurrentBook.ErrorsChanged += OnCurrentBookErrorsChanged;

        SelectUnselectBookCommand = new(book => SelectUnselectBook(book!));
        DeleteBookCommand = new(bookId => collection.Delete(bookId));
        AddBookCommand = new(AddBook);
        UpdateBookCommand = new(UpdateBook);
    }

    private void OnCurrentBookErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
        SetProperty(ref canAdd, !CurrentBook.HasErrors, nameof(CanAdd));
        SetProperty(ref canUpdate, selectedBook != null && canAdd, nameof(CanUpdate));
    }

    private void SelectUnselectBook(Book clickedBook)
    {
        // No book is selected
        if (SelectedBook == null)
        {
            SelectedBook = clickedBook;
            SelectedBook.IsSelected = true;
            CurrentBook = new(SelectedBook);
            CurrentBook.ErrorsChanged += OnCurrentBookErrorsChanged;
            return;
        }
        // A book is selected and it is this one
        if (SelectedBook.Id == clickedBook.Id)
        {
            SelectedBook.IsSelected = false;
            SelectedBook = null;
            CurrentBook = new();
            CurrentBook.ErrorsChanged += OnCurrentBookErrorsChanged;
            return;
        }
        // A book is selected but it is another book
        SelectedBook.IsSelected = false;
        SelectedBook = clickedBook;
        SelectedBook.IsSelected = true;
        CurrentBook = new(clickedBook);
        CurrentBook.ErrorsChanged += OnCurrentBookErrorsChanged;
    }

    private void AddBook()
    {
        var newBook = new Book() { Title = CurrentBook.Title, AuthorName = CurrentBook.AuthorName };
        collection.Add(newBook);
        CurrentBook = new();
        CurrentBook.ErrorsChanged += OnCurrentBookErrorsChanged;
    }

    private void UpdateBook()
    {
        collection.Update(CurrentBook);
        CurrentBook = new();
        CurrentBook.ErrorsChanged += OnCurrentBookErrorsChanged;
    }
}
