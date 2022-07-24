namespace Libretto.WPF.ViewModels;

using System.Collections.ObjectModel;

using Models;

public class MainViewModel
{
    public ObservableCollection<Book> Books { get; set; } = new();

    public MainViewModel()
    {
        SeedData();
    }

    private void SeedData()
    {
        Books.Add(new Book() { Title = "1984", AuthorName = "George Orwell" });
        Books.Add(new Book() { Title = "Crime and Punishment", AuthorName = "Fyodor Dostoevsky" });
        Books.Add(new Book() { Title = "Count Monte Cristo", AuthorName = "Alexandre Dumas" });   
    }
}
