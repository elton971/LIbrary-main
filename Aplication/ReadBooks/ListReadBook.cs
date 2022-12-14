using System.Net;
using Aplication.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.ReadBooks;

public class GetReadBookById
{
    public class ListReadBookQuery : IRequest<List<ReadBook>>
    {
        public int BookId { get; set; }
    }

    public class ListReadBookQueryHandler : IRequestHandler<ListReadBookQuery, List<ReadBook>>
    {
        private readonly DataContext _context;

        public ListReadBookQueryHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ReadBook>> Handle(ListReadBookQuery request, CancellationToken cancellationToken)
        {
            return await _context.ReadBooks.ToListAsync(cancellationToken) ;
        }
    }
}