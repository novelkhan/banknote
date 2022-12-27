using banknote.Data;
using banknote.Interfaces;
using banknote.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace banknote.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context = null;
        private readonly IConfiguration _configuration;

        public BookRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                Category = "Book"
                //BookPdfUrl = model.BookPdfUrl
            };

            newBook.bookGallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;

        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                  .Select(book => new BookModel()
                  {
                      Author = book.Author,
                      Category = book.Category,
                      Description = book.Description,
                      Id = book.Id,
                      LanguageId = book.LanguageId,
                      Language = book.Language.Name,
                      Title = book.Title,
                      TotalPages = book.TotalPages,
                      CoverImageUrl = book.CoverImageUrl
                  }).ToListAsync();



            //var books = new List<BookModel>();
            //var allbooks = await _context.Books.ToListAsync();

            //if (allbooks?.Any() == true)
            //{
            //    foreach (var book in allbooks)
            //    {
            //        books.Add(new BookModel()
            //        {
            //            Author = book.Author,
            //            Category = book.Category,
            //            Description = book.Description,
            //            Title = book.Title,
            //            Id = book.Id,
            //            LanguageId = book.LanguageId,
            //            TotalPages = book.TotalPages
            //        });
            //    }
            //}
            //return books;
        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books
                  .Select(book => new BookModel()
                  {
                      Author = book.Author,
                      Category = book.Category,
                      Description = book.Description,
                      Id = book.Id,
                      LanguageId = book.LanguageId,
                      Language = book.Language.Name,
                      Title = book.Title,
                      TotalPages = book.TotalPages,
                      CoverImageUrl = book.CoverImageUrl
                  }).Take(count).ToListAsync();
        }

        public async Task<BookModel> GetBookById(int id)
        {
            //return await _context.Books.Where(x => x.Id == id)
            //     .Select(book => new BookModel()
            //     {
            //         Author = book.Author,
            //         Category = book.Category,
            //         Description = book.Description,
            //         Id = book.Id,
            //         LanguageId = book.LanguageId,
            //         Language = book.Language.Name,
            //         Title = book.Title,
            //         TotalPages = book.TotalPages,
            //         //CoverImageUrl = book.CoverImageUrl,
            //         //Gallery = book.bookGallery.Select(g => new GalleryModel()
            //         //{
            //         //    Id = g.Id,
            //         //    Name = g.Name,
            //         //    URL = g.URL
            //         //}).ToList(),
            //         //BookPdfUrl = book.BookPdfUrl
            //     }).FirstOrDefaultAsync();



            //var book = await _context.Books.FindAsync(id);
            //if (book != null)
            //{
            //    var bookDetails = new BookModel()
            //    {
            //        Author = book.Author,
            //        Category = book.Category,
            //        Description = book.Description,
            //        Title = book.Title,
            //        Id = book.Id,
            //        LanguageId = book.LanguageId,
            //        TotalPages = book.TotalPages
            //    };

            //    return bookDetails;
            //}

            //return null;

            #pragma warning disable CS8603 // Possible null reference return.
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Title = book.Title,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL= g.URL
                    }).ToList()
                }).FirstOrDefaultAsync();
                #pragma warning restore CS8603 // Possible null reference return.
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }

        public string GetAppName()
        {
            return _configuration["AppName"];
        }
    }
}
