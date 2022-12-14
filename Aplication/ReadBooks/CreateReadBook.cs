using System.Net;
using Aplication.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.ReadBooks;

public class CreateReadBook
{
    public class CreateReadBookCommand : IRequest<ReadBook>
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
    }

    public class CreateReadBookCommandHandler : IRequestHandler<CreateReadBookCommand, ReadBook>
    {
        private readonly DataContext _context;

        public CreateReadBookCommandHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<ReadBook> Handle(CreateReadBookCommand request, CancellationToken cancellationToken)
        {
            var readBook = await _context.ReadBooks.FirstOrDefaultAsync(
                data => data.BookId == request.BookId && data.UserId == request.UserId,
                cancellationToken: cancellationToken);

            if (readBook is not null)
                throw new RestException(HttpStatusCode.Created, "Already read this book");

            readBook.BookId = request.BookId;
            readBook.UserId = request.UserId;
            
            await _context.Set<ReadBook>().AddAsync(readBook, cancellationToken);

            var result = await _context.SaveChangesAsync(cancellationToken) < 0;

            if (result)
                throw new Exception("An error occurred");

            return readBook;
        }
    }
}