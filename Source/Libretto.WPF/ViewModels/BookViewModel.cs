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
    public ReadOnlyObservableCollection<Book> Books => collection.Books;

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

        SetCurrentBook(null);

        SelectUnselectBookCommand = new(book => SelectUnselectBook(book!));
        DeleteBookCommand = new(DeleteBook);
        AddBookCommand = new(AddBook);
        UpdateBookCommand = new(UpdateBook);
    }

    private void SetCurrentBook(Book? book)
    {
        if (book == null)
        {
            CurrentBook = new();
        }
        else
        {
            CurrentBook = new(book);
        }
        CurrentBook.ErrorsChanged += OnCurrentBookErrorsChanged;
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
            SetCurrentBook(SelectedBook);
            return;
        }
        // A book is selected and it is this one
        if (SelectedBook.Id == clickedBook.Id)
        {
            SelectedBook.IsSelected = false;
            SelectedBook = null;
            SetCurrentBook(null);
            return;
        }
        // A book is selected but it is another book
        SelectedBook.IsSelected = false;
        SelectedBook = clickedBook;
        SelectedBook.IsSelected = true;
        SetCurrentBook(clickedBook);
    }

    private void DeleteBook(Guid bookId)
    {
        if (SelectedBook != null && SelectedBook.Id == bookId)
        {
            SelectedBook = null;
        }
        collection.Delete(bookId);
    }

    private void AddBook()
    {
        var newBook = new Book() { Title = CurrentBook.Title, AuthorName = CurrentBook.AuthorName };
        collection.Add(newBook);
        if (SelectedBook != null)
        {
            SelectedBook.IsSelected = false;
            SelectedBook = null;
        }
        SetCurrentBook(null);
    }

    private void UpdateBook()
    {
        collection.Update(CurrentBook);
        if (SelectedBook != null)
        {
            SelectedBook.IsSelected = false;
            SelectedBook = null;
        }
        SetCurrentBook(null);
    }
}
