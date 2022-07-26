namespace Libretto.WPF.Services;

using System;
using System.Collections.ObjectModel;
using System.Linq;

using Libretto.WPF.Models;

public class BookCollection : IBookCollection
{
    private ObservableCollection<Book> books;
    public ReadOnlyObservableCollection<Book> Books { get; private set; }

    public BookCollection()
    {
        books = new();
        Books = new(books);
    }

    public void SeedData()
    {
        for (int i = 0; i < 20; ++i)
        {
            books.Add(new Book() { Title = "1984", AuthorName = "George Orwell" });
            books.Add(new Book() { Title = "Crime and Punishment", AuthorName = "Fyodor Dostoevsky" });
            books.Add(new Book() { Title = "Count Monte Cristo", AuthorName = "Alexandre Dumas" });
        }
    }

    public void Add(Book newBook)
    {
        books.Add(newBook);
    }

    public void Delete(Guid bookId)
    {
        var bookToRemove = Books.Single(b => b.Id == bookId);
        books.Remove(bookToRemove);
    }

    public void DeleteAll() => books.Clear();

    public void Update(Book updatedBook)
    {
        var bookToUpdate = Books.Single(b => b.Id == updatedBook.Id);
        int index = Books.IndexOf(bookToUpdate);
        books.RemoveAt(index);
        books.Insert(index, updatedBook);
    }
}
