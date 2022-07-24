namespace Libretto.WPF.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Models;

public class MainViewModel : ObservableObject
{
    public ObservableCollection<Book> Books { get; set; }

    private RelayCommand<Guid>? deleteBookCommand;
    public RelayCommand<Guid>? DeleteBookCommand
    {
        get => deleteBookCommand;
        set => SetProperty(ref deleteBookCommand, value);
    }

    public MainViewModel()
    {
        Books = new();
        SeedData();

        DeleteBookCommand = new(bookId => DeleteBook(bookId));
    }

    private void DeleteBook(Guid bookId)
    {
        var bookToRemove = Books.Single(b => b.Id == bookId);
        Books.Remove(bookToRemove);
    }

    private void SeedData()
    {
        Books.Add(new Book() { Title = "1984", AuthorName = "George Orwell" });
        Books.Add(new Book() { Title = "Crime and Punishment", AuthorName = "Fyodor Dostoevsky" });
        Books.Add(new Book() { Title = "Count Monte Cristo", AuthorName = "Alexandre Dumas" });   
    }
}
