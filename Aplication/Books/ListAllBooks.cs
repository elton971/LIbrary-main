using System.Net;
using Aplication.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.Books;

public class ListAllBooks
{
    public class ListAllBooksQuery:IRequest<List<Book>>
    {
        
    }
    
    public class ListAllBooksQueryHandle:IRequestHandler<ListAllBooksQuery,List<Book>>
    {
        private readonly DataContext _context;

        public ListAllBooksQueryHandle(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<Book>> Handle(ListAllBooksQuery request, CancellationToken cancellationToken)
        {
           var books = await _context.Books.ToListAsync(cancellationToken);

           if (books is null)
               throw new RestException(HttpStatusCode.NotFound, "Haven't books");
           return books;
        }
    }
}