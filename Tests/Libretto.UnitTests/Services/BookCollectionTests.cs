namespace Libretto.UnitTests.Services;

using FluentAssertions;

using WPF.Models;
using WPF.Services;

public class BookCollectionTests
{
    [Fact]
    public void SeedData_CollectionMustBeNonEmpty()
    {
        // Arrange
        IBookCollection bookCollection = new BookCollection();

        // Act
        bookCollection.SeedData();

        // Assert
        bookCollection.Books.Should().NotBeEmpty();
    }

    [Fact]
    public void DeleteAll_CollectionMustBeEmpty()
    {
        // Arrange
        IBookCollection bookCollection = new BookCollection();
        bookCollection.SeedData();

        // Act
        bookCollection.DeleteAll();

        // Assert
        bookCollection.Books.Should().BeEmpty();
    }

    [Fact]
    public void Add_BookMustBeAddedToCollection()
    {
        // Arrange
        IBookCollection bookCollection = new BookCollection();
        bookCollection.DeleteAll();

        // Act
        var book = new Book() { Title = "TestBook", AuthorName = "TestAuthor" };
        bookCollection.Add(book);

        // Assert
        bookCollection.Books.Should().Contain(book);
    }

    [Fact]
    public void Delete_BookMustBeRemovedFromCollection()
    {
        // Arrange
        IBookCollection bookCollection = new BookCollection();
        var book = new Book() { Title = "TestBook", AuthorName = "TestAuthor" };
        bookCollection.Add(book);

        // Act
        bookCollection.Delete(book.Id);

        // Assert
        bookCollection.Books.Should().NotContain(book);
    }

    [Fact]
    public void Update_BookMustBeUpdated()
    {
        // Arrange
        IBookCollection bookCollection = new BookCollection();
        var book = new Book() { Title = "TestBook", AuthorName = "TestAuthor" };
        bookCollection.Add(book);

        // Act
        book.Title = "Updated";
        book.AuthorName = "Updated";
        bookCollection.Update(book);
        var actualBook = bookCollection.Books.Single(b => b.Id == book.Id);

        // Assert
        bookCollection.Books.Should().Contain(book);
        actualBook.Should().BeEquivalentTo(book);
    }
}
