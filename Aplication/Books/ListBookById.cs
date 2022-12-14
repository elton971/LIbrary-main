using System.Net;
using Aplication.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.Books;

public class GetBookById
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }
    }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly DataContext _context;

        public GetBookByIdQueryHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == request.Id);
            if (book is null)
                throw new RestException(HttpStatusCode.NotFound,"Book not found");
            
            return book;
        }
    }
}