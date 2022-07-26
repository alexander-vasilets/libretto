namespace Libretto.WPF.Services;

using System;
using System.Collections.ObjectModel;

using Models;

public interface IBookCollection
{
    ReadOnlyObservableCollection<Book> Books { get; }

    void SeedData();
    void Add(Book newBook);
    void Delete(Guid bookId);
    void DeleteAll();
    void Update(Book updatedBook);
}
