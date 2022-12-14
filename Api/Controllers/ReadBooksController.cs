using Aplication.ReadBooks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ReadBooksController: BaseController
{
    private readonly IMediator _mediator;

    public ReadBooksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<ReadBook>> CreateReadBook(CreateReadBook.CreateReadBookCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<List<ReadBook>>> ListAllReadBooks()
    {
        return await _mediator.Send(new GetReadBookById.ListReadBookQuery());
    }
}