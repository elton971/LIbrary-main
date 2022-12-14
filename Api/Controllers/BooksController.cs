using Aplication.Books;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


public class BooksController:BaseController
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(AddBook.AddBooKCommand bookCommand)
    {
        return await _mediator.Send(bookCommand);
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAllBooks()
    {
        return await _mediator.Send(new ListAllBooks.ListAllBooksQuery());
    }
    
    [HttpGet("{id}")]
    public async Task<Book> GetBookById(int id)
    {
        return await _mediator.Send(new GetBookById.GetBookByIdQuery{Id = id});
    }

    [HttpPut("{id}")]
    public async Task<Book> UpdateBook(UpdateBook.UpdateBookCommand command, int id)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<Book> DeleteBook(int id)
    {
        return await _mediator.Send(new DeleteBook.DeleteBookCommand{Id = id});
    }
}