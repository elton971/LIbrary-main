using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.Books;

public class DeleteBook
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly DataContext _context;

        public DeleteBookCommandHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == request.Id);
            if (book is null)
                throw new Exception("Book not found");
            _context.Books.Remove(book);
            
            var result = await _context.SaveChangesAsync(cancellationToken) < 0;
            if (result)
            {
                throw new Exception("An Error Occurred while updating");
            }
            
            return book;
        }
    }
}