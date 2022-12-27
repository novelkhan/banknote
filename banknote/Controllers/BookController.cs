using banknote.Interfaces;
using banknote.Repository;
using banknote.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace banknote.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel();

            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            ModelState.Remove(nameof(bookModel.Category));
            ModelState.Remove(nameof(bookModel.Language));
            ModelState.Remove(nameof(bookModel.CoverImageUrl));
            ModelState.Remove(nameof(bookModel.Gallery));


            //if (ModelState.IsValid)
            //{
            //    //go on as normal
            //}
            //else
            //{
            //    var errors = ModelState.Select(x => x.Value.Errors)
            //                           .Where(y => y.Count > 0)
            //                           .ToList();
            //}

            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
                }


                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();
                    
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.Name,
                            URL = await UploadImage(folder, file)
                        };

                        bookModel.Gallery.Add(gallery);
                    }
                }






                //if (bookModel.CoverPhoto != null)
                //{
                //    string folder = "books/cover/";
                //    folder += Guid.NewGuid().ToString() + "_" + bookModel.CoverPhoto.FileName;
                //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                //    bookModel.CoverImageUrl = "/" + folder;

                //    await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                //}







                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }


            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            return View();
        }



        

        [Route("all-books")]
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();

            return View(data);
        }

        [Route("book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);

            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }



        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }






































        //[Route("all-books")]
        //public async Task<ViewResult> GetAllBooks()
        //{
        //    var data = await _bookRepository.GetAllBooks();

        //    return View(data);
        //}

        //[Route("book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        //public async Task<ViewResult> GetBook(int id)
        //{
        //    var data = await _bookRepository.GetBookById(id);

        //    return View(data);
        //}

        //public List<BookModel> SearchBooks(string bookName, string authorName)
        //{
        //    return _bookRepository.SearchBook(bookName, authorName);
        //}

        //[Authorize]
        //public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        //{
        //    var model = new BookModel();

        //    ViewBag.IsSuccess = isSuccess;
        //    ViewBag.BookId = bookId;
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddNewBook(BookModel bookModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (bookModel.CoverPhoto != null)
        //        {
        //            string folder = "books/cover/";
        //            bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
        //        }

        //        if (bookModel.GalleryFiles != null)
        //        {
        //            string folder = "books/gallery/";

        //            bookModel.Gallery = new List<GalleryModel>();

        //            foreach (var file in bookModel.GalleryFiles)
        //            {
        //                var gallery = new GalleryModel()
        //                {
        //                    Name = file.FileName,
        //                    URL = await UploadImage(folder, file)
        //                };
        //                bookModel.Gallery.Add(gallery);
        //            }
        //        }

        //        if (bookModel.BookPdf != null)
        //        {
        //            string folder = "books/pdf/";
        //            bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
        //        }

        //        int id = await _bookRepository.AddNewBook(bookModel);
        //        if (id > 0)
        //        {
        //            return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
        //        }
        //    }

        //    return View();
        //}

        //private async Task<string> UploadImage(string folderPath, IFormFile file)
        //{

        //    folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

        //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

        //    await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

        //    return "/" + folderPath;
        //}
    }
}
