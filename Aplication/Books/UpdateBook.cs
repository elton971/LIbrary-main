using System.Net;
using Aplication.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.Books;

public class UpdateBook
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
        public string Title  { get; set; }
        public string Author  { get; set; }
        public string Publishing_company  { get; set; }
        public int Year { get; set; }
        public string Description  { get; set; }
        public string Image  { get; set; }
        public string Url_Download  { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly DataContext _context;

        public UpdateBookCommandHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == request.Id);
            
            if (book is null)
                throw new RestException(HttpStatusCode.NotFound,"Book not found");
            
            book.Author = request.Author;
            book.Title = request.Title;
            book.Publishing_company = request.Publishing_company;
            book.Year = request.Year;
            book.Description = request.Description;
            book.Image = request.Image;
            book.Url_Download = request.Url_Download;

            _context.Books.Update(book);

            var result = await _context.SaveChangesAsync(cancellationToken) < 0;
            if (result)
            {
                throw new Exception("An Error Occurred");
            }
            return book;
        }
    }
}