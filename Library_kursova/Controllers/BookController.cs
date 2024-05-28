using Library_kursova.Data;
using Library_kursova.DTO;
using Library_kursova.Entities;
using Library_kursova.Extensions;
using Library_kursova.Helpers;
using Library_kursova.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private LibraryContext _libraryContext;
        private readonly IPhotoService _photoService;
        public BookController(LibraryContext context, IPhotoService photoService)
        {
            _libraryContext = context;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<BookDTO>>> GetBooks([FromQuery]BookParams bookParams)
        {
            var query = _libraryContext.Book
                .Include(b => b.BookAuthors)
              .ThenInclude(ab => ab.Author) // Include the Author entity through AuthorBook
            .AsQueryable();

            if (bookParams.Type == "like")
            {
                query = query.OrderByDescending(b => b.NumberOfLikes);
            }
            else if (bookParams.Type == "new")
            {
                query = query.OrderByDescending(b => b.CreatedDate);
            }

            var books = await PagedList<BookDTO>.CreateAsync(query.AsNoTracking().Select(b => new BookDTO
            {
                Book = b,
                AuthorNames = b.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                Language = _libraryContext.Language.FirstOrDefault(l => l.LanguageId == b.LanguageId).Name,
                Category = _libraryContext.Category.Where(l => l.CategoryId == b.CategoryId).FirstOrDefault().Name,
                PublishingHouse = _libraryContext.PublishingHouse.Where(l => l.PublishingHouseId == b.PublishingHouseId).FirstOrDefault().Name,
            }), bookParams.PageNumber, bookParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(books.CurrentPage, books.PageSize, books.TotalCount, books.TotalPages));

            return Ok(books);
        }

        [HttpPost("filter")]
        public async Task<ActionResult<PagedList<BookDTO>>> FilterBooks ([FromQuery] BookParams bookParams, FilterDTO filterDTO)
        {
            var query = _libraryContext.Book
                .Include(b => b.BookAuthors)
              .ThenInclude(ab => ab.Author) // Include the Author entity through AuthorBook
            .AsQueryable();

            if (filterDTO.Categories != null && filterDTO.Categories.Any())
            {
                query = query.Where(b => filterDTO.Categories.Contains(b.Category.Name));
            }

            if (filterDTO.PublishingHouses != null && filterDTO.PublishingHouses.Any())
            {
                query = query.Where(b => filterDTO.PublishingHouses.Contains(b.PublishingHouse.Name));
            }

            if (filterDTO.Languages != null && filterDTO.Languages.Any())
            {
                query = query.Where(b => filterDTO.Languages.Contains(b.Language.Name));
            }

            if (bookParams.Type == "like")
            {
                query = query.OrderByDescending(b => b.NumberOfLikes);
            }
            else if (bookParams.Type == "new")
            {
                query = query.OrderByDescending(b => b.CreatedDate);
            }

            var books = await PagedList<BookDTO>.CreateAsync(query.AsNoTracking().Select(b => new BookDTO
            {
                Book = b,
                AuthorNames = b.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                Language = _libraryContext.Language.FirstOrDefault(l => l.LanguageId == b.LanguageId).Name,
                Category = _libraryContext.Category.Where(l => l.CategoryId == b.CategoryId).FirstOrDefault().Name,
                PublishingHouse = _libraryContext.PublishingHouse.Where(l => l.PublishingHouseId == b.PublishingHouseId).FirstOrDefault().Name,
            }), bookParams.PageNumber, bookParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(books.CurrentPage, books.PageSize, books.TotalCount, books.TotalPages));

            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var comments = await _libraryContext.Comment
                .Where(b => b.BookId == id)
                .Include(c => c.User)
                .Select(c => new CommentResponseDTO
                {
                    NickName = c.User.Nickname,
                    UserImage = c.User.UserImage,
                    Text = c.Text,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();

            var bookDTO = await _libraryContext.Book
                .Where(b => b.BookId == id)
                .Include(b => b.BookAuthors)
                .ThenInclude(ab => ab.Author)
                .Select(b => new BookDTO
                {
                    Book = b,
                    AuthorNames = b.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                    Language = _libraryContext.Language.FirstOrDefault(l => l.LanguageId == b.LanguageId).Name,
                    Category = _libraryContext.Category.Where(l => l.CategoryId == b.CategoryId).FirstOrDefault().Name,
                    PublishingHouse = _libraryContext.PublishingHouse.Where(l => l.PublishingHouseId == b.PublishingHouseId).FirstOrDefault().Name,
                    Comments = comments
                }).FirstOrDefaultAsync();

            if (bookDTO == null)
            {
                return NotFound(); // Return 404 if book with given ID is not found
            }

            

            return Ok(bookDTO);
        }

        [HttpGet("novelties")]
        public async Task<ActionResult<IEnumerable<Book>>> GetNoveltiesBook()
        {
            var books = await _libraryContext.Book.ToListAsync();
            //books.OrderBy(b => b.CreatedDate).Take(3);

            return books.OrderByDescending(b => b.CreatedDate).Take(10).ToList();
        }

        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetPopularBook()
        {
            var bookDTOs = await _libraryContext.Book
                .Include(b => b.BookAuthors)
              .ThenInclude(ab => ab.Author) // Include the Author entity through AuthorBook
            .Select(b => new BookDTO
            {
                Book = b,
                AuthorNames = b.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                Language = _libraryContext.Language.FirstOrDefault(l => l.LanguageId == b.LanguageId).Name,
                Category = _libraryContext.Category.Where(l => l.CategoryId == b.CategoryId).FirstOrDefault().Name,
                PublishingHouse = _libraryContext.PublishingHouse.Where(l => l.PublishingHouseId == b.PublishingHouseId).FirstOrDefault().Name,
            })
            .ToListAsync();
            //books.OrderBy(b => b.CreatedDate).Take(3);

            return bookDTOs.OrderByDescending(b => b.Book.NumberOfLikes).Take(4).ToList();
        }

        [HttpGet("random")]
        public async Task<ActionResult<BookDTO>> GetRandomBook()
        {
            var bookDTOs = await _libraryContext.Book
                .Include(b => b.BookAuthors)
              .ThenInclude(ab => ab.Author) // Include the Author entity through AuthorBook
            .Select(b => new BookDTO
            {
                Book = b,
                AuthorNames = b.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                Language = _libraryContext.Language.FirstOrDefault(l => l.LanguageId == b.LanguageId).Name,
                Category = _libraryContext.Category.Where(l => l.CategoryId == b.CategoryId).FirstOrDefault().Name,
                PublishingHouse = _libraryContext.PublishingHouse.Where(l => l.PublishingHouseId == b.PublishingHouseId).FirstOrDefault().Name,
            })
            .ToListAsync();
            var random = new Random();
            int randomIndex = random.Next(0, bookDTOs.Count);
            //books.OrderBy(b => b.CreatedDate).Take(3);

            return bookDTOs[randomIndex];
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetSearchingBooks(string searchString)
        {
            var bookDTOs = await _libraryContext.Book
                .Include(b => b.BookAuthors)
              .ThenInclude(ab => ab.Author) // Include the Author entity through AuthorBook
            .Select(b => new BookDTO
            {
                Book = b,
                AuthorNames = b.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                Language = _libraryContext.Language.FirstOrDefault(l => l.LanguageId == b.LanguageId).Name,
                Category = _libraryContext.Category.Where(l => l.CategoryId == b.CategoryId).FirstOrDefault().Name,
                PublishingHouse = _libraryContext.PublishingHouse.Where(l => l.PublishingHouseId == b.PublishingHouseId).FirstOrDefault().Name,
            })
            .ToListAsync();

            if(!String.IsNullOrEmpty(searchString))
            {
                bookDTOs = bookDTOs.Where(b => b.Book.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return bookDTOs;
        }


        [HttpPost("add-book-request")]
        public async Task<ActionResult> AddBook(BookDTO bookDTO)
        {
            bookDTO.Book.CreatedDate = DateTime.UtcNow;
            
            if (ModelState.IsValid)
            {
                var book = bookDTO.Book;

                bookDTO.Book.LanguageId = await GetLanguageIdByNameAsync(bookDTO.Language);
                bookDTO.Book.CategoryId = await GetCategoryIdByNameAsync(bookDTO.Category);
                bookDTO.Book.PublishingHouseId = await GetPublishingHoyseIdByNameAsync(bookDTO.PublishingHouse);
                await _libraryContext.Book.AddAsync(book);
                await _libraryContext.SaveChangesAsync();

                foreach (string authorName in bookDTO.AuthorNames)
                {
                    int authorId = await GetAuthorIdByNameAsync(authorName);
                    if (authorId != -1)
                    {
                        var authorBook = new AuthorBook {AuthorId = authorId, BookId = book.BookId };
                        await _libraryContext.AuthorBook.AddAsync(authorBook);
                        await _libraryContext.SaveChangesAsync();
                    }
                    else
                    {
                        return BadRequest("Author doesn't exist");
                    }
                }
                await _libraryContext.SaveChangesAsync();
                return Ok();
            }

            var message = GetModelValidationErrors();

            return BadRequest(message);
        }

        [HttpPost("add-photo-book")]
        public async  Task<ActionResult<string>> addPhotoBook(IFormFile file)
        {
            var result = await _photoService.AddPhotoAsync(/*photoRequestDTO.*/file, "book");

            if (result.Error != null) return BadRequest(result.Error);

            return Content(result.SecureUrl.AbsoluteUri, "text/plain");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateBook(BookDTO bookDTO)
        {
            if (bookDTO.Book.BookId == default || !BookExists(bookDTO.Book.BookId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            bookDTO.Book.LanguageId = await GetLanguageIdByNameAsync(bookDTO.Language);
            bookDTO.Book.CategoryId = await GetCategoryIdByNameAsync(bookDTO.Category);
            bookDTO.Book.PublishingHouseId = await GetPublishingHoyseIdByNameAsync(bookDTO.PublishingHouse);
            _libraryContext.Book.Update(bookDTO.Book);

            var bookAuthorToDelete = GetAllBookAuthors(bookDTO.Book.BookId);
            foreach (var authorName in bookDTO.AuthorNames)
            {
                var authorId = await GetAuthorIdByNameAsync(authorName);
                if (authorId != -1 && await AuthorBookExists(bookDTO.Book.BookId, authorId) == false)
                {
                    var authorBook = new AuthorBook { AuthorId = authorId, BookId = bookDTO.Book.BookId };
                    await _libraryContext.AuthorBook.AddAsync(authorBook);
                }
                bookAuthorToDelete.Remove(authorId);
            }
            foreach(var authorToDeleteId in bookAuthorToDelete)
            {
                var authorBook = await _libraryContext.AuthorBook.FirstOrDefaultAsync(e => e.BookId == bookDTO.Book.BookId && e.AuthorId == authorToDeleteId);
                _libraryContext.AuthorBook.Remove(authorBook);
            }

            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _libraryContext.Book.FirstOrDefaultAsync(l => l.BookId == id);

            if (book == default) return NotFound();

            _libraryContext.Book.Remove(book);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        private List<int> GetAllBookAuthors(int bookId)
        {
            var bookAuthors = _libraryContext.AuthorBook.Where(er => er.BookId == bookId).ToList();
            var authors = new List<int>();
            foreach (var bookAuthor in bookAuthors)
            {
                authors.Add(bookAuthor.AuthorId);
            }
            return authors;
        }

        private async Task<bool> AuthorBookExists(int bookId, int authorId)
        {
            return await _libraryContext.AuthorBook.AnyAsync(ri => ri.AuthorId == authorId && ri.BookId == bookId);
        }

        public async Task<int> GetAuthorIdByNameAsync(string authorName)
        {
            var author = await _libraryContext.Author
                .FirstOrDefaultAsync(a => a.Pseudonym == authorName);

            if (author != null)
            {
                return author.AuthorId;
            }
            else
            {
                // Handle the case where the author is not found
                return -1; // Or throw an exception, return null, etc.
            }
        }

        public async Task<int> GetLanguageIdByNameAsync(string languageName)
        {
            var lang = await _libraryContext.Language
                .FirstOrDefaultAsync(a => a.Name == languageName);

            if (lang != null)
            {
                return lang.LanguageId;
            }
            else
            {
                // Handle the case where the author is not found
                return -1; // Or throw an exception, return null, etc.
            }
        }

        public async Task<int> GetCategoryIdByNameAsync(string categoryName)
        {
            var category = await _libraryContext.Category
                .FirstOrDefaultAsync(a => a.Name == categoryName);

            if (category != null)
            {
                return category.CategoryId;
            }
            else
            {
                // Handle the case where the author is not found
                return -1; // Or throw an exception, return null, etc.
            }
        }

        public async Task<int> GetPublishingHoyseIdByNameAsync(string phName)
        {
            var house = await _libraryContext.PublishingHouse
                .FirstOrDefaultAsync(a => a.Name == phName);

            if (house != null)
            {
                return house.PublishingHouseId;
            }
            else
            {
                // Handle the case where the author is not found
                return -1; // Or throw an exception, return null, etc.
            }
        }

        public async Task<string> GetUserNickNameByIdAsync(int id)
        {
            var user = await _libraryContext.Users
                .FirstOrDefaultAsync(a => a.Id == id);

            if (user != null)
            {
                return user.Nickname;
            }
            else
            {
                // Handle the case where the author is not found
                return ""; // Or throw an exception, return null, etc.
            }
        }

        public async Task<string> GetUserPhotoByIdAsync(int id)
        {
            var user = await _libraryContext.Users
                .FirstOrDefaultAsync(a => a.Id == id);

            if (user != null)
            {
                return user.UserImage;
            }
            else
            {
                // Handle the case where the author is not found
                return ""; // Or throw an exception, return null, etc.
            }
        }

        private bool BookExists(int id)
        {
            return _libraryContext.Book.Any(e => e.BookId == id);
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
