namespace Libretto.WPF.Services;

using System;
using System.Collections.ObjectModel;
using System.Linq;

using Libretto.WPF.Models;

public class BookCollection : IBookCollection
{
    public ObservableCollection<Book> Books { get; } = new();

    public void SeedData()
    {
        Books.Add(new Book() { Title = "1984", AuthorName = "George Orwell" });
        Books.Add(new Book() { Title = "Crime and Punishment", AuthorName = "Fyodor Dostoevsky" });
        Books.Add(new Book() { Title = "Count Monte Cristo", AuthorName = "Alexandre Dumas" });
    }

    public void Add(Book newBook)
    {
        Books.Add(newBook);
    }

    public void Delete(Guid bookId)
    {
        var bookToRemove = Books.Single(b => b.Id == bookId);
        Books.Remove(bookToRemove);
    }

    public void DeleteAll() => Books.Clear();
}
