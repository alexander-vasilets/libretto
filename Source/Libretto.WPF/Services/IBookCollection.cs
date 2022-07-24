namespace Libretto.WPF.Services;

using System;
using System.Collections.ObjectModel;

using Libretto.WPF.Models;

public interface IBookCollection
{
    ObservableCollection<Book> Books { get; }

    void SeedData();
    void Add(Book newBook);
    void Delete(Guid bookId);
    void DeleteAll();
}
