using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Book> books = new List<Book>();

// GET all books
app.MapGet("/books", () => Results.Ok(books));

// GET a single book by ID
app.MapGet("/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.BookId == id);
    return book != null ? Results.Ok(book) : Results.NotFound("Book not found");
});

// POST a new book
app.MapPost("/books", (Book book) =>
{
    books.Add(book);
    return Results.Created($"/books/{book.BookId}", book);
});

// PUT (update) a book by ID
app.MapPut("/books/{id}", (int id, Book updatedBook) =>
{
    var book = books.FirstOrDefault(b => b.BookId == id);
    if (book == null)
        return Results.NotFound("Book not found");

    book.Author = updatedBook.Author;
    book.Title = updatedBook.Title;
    return Results.Ok(book);
});

// DELETE a book by ID
app.MapDelete("/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.BookId == id);
    if (book == null)
        return Results.NotFound("Book not found");

    books.Remove(book);
    return Results.Ok("Book deleted");
});

app.Run();

// Book model
public class Book
{
    public int BookId { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
}
