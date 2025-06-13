using Library.Data;
using Library.DTO;
using Library.Entities;
using Library.Extensions;
using Library.Helpers;
using Library.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library.Controllers
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
              .ThenInclude(ab => ab.Author)
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
              .ThenInclude(ab => ab.Author)
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
                     CreatedDate = c.CreatedDate,
                     UserId = c.UserId,
                 })
                 .ToListAsync();

            var book = await _libraryContext.Book
                .Include(b => b.BookAuthors)
                    .ThenInclude(ab => ab.Author)
                .Include(b => b.Copies)
                    .ThenInclude(c => c.BookBorrows)
                .Include(b => b.Copies)
                    .ThenInclude(c => c.Reservations)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
                return NotFound();

            var totalCopies = book.Copies.Count;
            var borrowedCopies = book.Copies.Count(c => c.BookBorrows.Any(b => !b.IsReturned));
            var reservedCopies = book.Copies.Count(c => c.Reservations.Any(r => r.IsActive));
            var availableCopies = totalCopies - borrowedCopies - reservedCopies;

            string status;
            if (availableCopies > 0)
                status = "Available";
            else if (reservedCopies > 0)
                status = "Reserved";
            else
                status = "Borrowed";

            var nearestReservation = book.Copies
                .SelectMany(c => c.Reservations)
                .Where(r => r.IsActive)
                .OrderBy(r => r.ExpiresAt)
                .FirstOrDefault();

            var bookDTO = new BookDTO
            {
                Book = book,
                AuthorNames = book.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                Language = _libraryContext.Language.FirstOrDefault(l => l.LanguageId == book.LanguageId)?.Name ?? "Unknown",
                Category = _libraryContext.Category.FirstOrDefault(c => c.CategoryId == book.CategoryId)?.Name ?? "Unknown",
                PublishingHouse = _libraryContext.PublishingHouse.FirstOrDefault(p => p.PublishingHouseId == book.PublishingHouseId)?.Name ?? "Unknown",
                Comments = comments,
                Copies = availableCopies,
                AvailabilityStatus = status,
                NearestReservationExpiry = nearestReservation?.ExpiresAt
            };

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
              .ThenInclude(ab => ab.Author)
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
              .ThenInclude(ab => ab.Author)
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
              .ThenInclude(ab => ab.Author)
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

        [HttpGet("{id}/similar")]
        public async Task<ActionResult<List<BookDTO>>> GetSimilarBooks(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:8000/similar/{id}");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Failed to get similar books.");

            var json = await response.Content.ReadAsStringAsync();
            var similarBooks = JsonConvert.DeserializeObject<List<SimilarBookDTO>>(json);

            var bookIds = similarBooks.Select(b => b.BookId).ToList();
            var books = await _libraryContext.Book
                .Include(b => b.BookAuthors)
                    .ThenInclude(ab => ab.Author)
                .Where(b => bookIds.Contains(b.BookId))
                .ToListAsync();

            var bookDTOs = books.Select(b => new BookDTO
            {
                Book = b
            }).ToList();

            return bookDTOs;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add-book-request")]
        public async Task<ActionResult> AddBook([FromBody]BookDTORequest bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingBook = await _libraryContext.Book
                .Include(b => b.BookAuthors)
                .ThenInclude(ab => ab.Author)
                .Include(b => b.PublishingHouse)
                .FirstOrDefaultAsync(b =>
                    b.Title == bookDTO.Book.Title &&
                    b.PublishingHouse.Name == bookDTO.PublishingHouse);

            if (existingBook == null)
            {
                var book = bookDTO.Book;
                book.CreatedDate = DateTime.UtcNow;

                bookDTO.Book.LanguageId = await GetLanguageIdByNameAsync(bookDTO.Language);
                bookDTO.Book.CategoryId = await GetCategoryIdByNameAsync(bookDTO.Category);
                bookDTO.Book.PublishingHouseId = await GetPublishingHouseIdByNameAsync(bookDTO.PublishingHouse);

                await _libraryContext.Book.AddAsync(book);
                await _libraryContext.SaveChangesAsync();

                foreach (string authorName in bookDTO.AuthorNames)
                {
                    int authorId = await GetAuthorIdByNameAsync(authorName);
                    if (authorId != -1)
                    {
                        var authorBook = new AuthorBook {AuthorId = authorId, BookId = book.BookId };
                        await _libraryContext.AuthorBook.AddAsync(authorBook);
                    }
                    else
                    {
                        return BadRequest("Author doesn't exist");
                    }
                }
                await _libraryContext.SaveChangesAsync();

                var bookCopy = new BookCopy
                {
                    BookId = book.BookId,
                };
                await _libraryContext.BookCopy.AddAsync(bookCopy);
            }
            else
            {
                var bookCopy = new BookCopy
                {
                    BookId = existingBook.BookId,
                };
                await _libraryContext.BookCopy.AddAsync(bookCopy);
            }

            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add-copy")]
        public async Task<ActionResult> AddBookCopy([FromBody] string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return BadRequest("ISBN is required");

            var book = await _libraryContext.Book.FirstOrDefaultAsync(x => x.ISBN == isbn);

            if (book == null)
                return NotFound("Book not found");

            var bookCopy = new BookCopy
            {
                BookId = book.BookId,
            };

            await _libraryContext.BookCopy.AddAsync(bookCopy);
            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add-photo-book")]
        public async  Task<ActionResult<string>> addPhotoBook(IFormFile file)
        {
            var result = await _photoService.AddPhotoAsync(/*photoRequestDTO.*/file, "book");

            if (result.Error != null) return BadRequest(result.Error);

            return Content(result.SecureUrl.AbsoluteUri, "text/plain");
        }

        [HttpGet("availablecopies/{bookId}")]
        public async Task<ActionResult<object>> GetAvailabilityInfo(int bookId)
        {
            var now = DateTime.UtcNow;

            var bookCopies = await _libraryContext.BookCopy
                .Where(bc => bc.BookId == bookId)
                .ToListAsync();

            int totalCopies = bookCopies.Count;

            int availableCount = 0;
            List<DateTime?> expirationDates = new();

            foreach (var copy in bookCopies)
            {
                var isBorrowed = await _libraryContext.BorrowsBook
                    .AnyAsync(bb => bb.BookCopyId == copy.BookCopyId &&
                                    !bb.IsReturned &&
                                    bb.ReturnDate > DateOnly.FromDateTime(now));

                var isReserved = _libraryContext.Reservation
                    .AsEnumerable()
                    .Any(r => r.BookCopyId == copy.BookCopyId &&
                              r.IsActive &&
                              r.ExpiresAt > now);

                if (!isBorrowed && !isReserved)
                {
                    availableCount++;
                }
                else
                {
                    var returnDate = await _libraryContext.BorrowsBook
                        .Where(bb => bb.BookCopyId == copy.BookCopyId && !bb.IsReturned)
                        .Select(bb => (DateTime?)bb.ReturnDate.ToDateTime(TimeOnly.MinValue))
                        .FirstOrDefaultAsync();

                    var reservationDate = await _libraryContext.Reservation
                        .Where(r => r.BookCopyId == copy.BookCopyId && r.IsActive)
                        .Select(r => (DateTime?)r.ExpiresAt)
                        .FirstOrDefaultAsync();

                    if (returnDate.HasValue)
                        expirationDates.Add(returnDate);
                    if (reservationDate.HasValue)
                        expirationDates.Add(reservationDate);
                }
            }

            var soonestExpiration = expirationDates
                .Where(d => d.HasValue)
                .OrderBy(d => d)
                .FirstOrDefault();

            return Ok(new
            {
                TotalCopies = totalCopies,
                AvailableCopies = availableCount,
                IsFullyOccupied = availableCount == 0,
                SoonestAvailableDate = soonestExpiration?.ToString("u") ?? null
            });
        }

        [HttpGet("exists")]
        public async Task<ActionResult<bool>> BookExists([FromQuery] string isbn)
        {
            var book = await _libraryContext.Book
                .Include(b => b.PublishingHouse)
                .FirstOrDefaultAsync(b =>
                    b.ISBN == isbn);

            return Ok(book != null);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-book")]
        public async Task<ActionResult> UpdateBook([FromBody]BookDTORequest bookDTO)
        {
            if (bookDTO.Book.BookId == default || !BookExists(bookDTO.Book.BookId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            bookDTO.Book.LanguageId = await GetLanguageIdByNameAsync(bookDTO.Language);
            bookDTO.Book.CategoryId = await GetCategoryIdByNameAsync(bookDTO.Category);
            bookDTO.Book.PublishingHouseId = await GetPublishingHouseIdByNameAsync(bookDTO.PublishingHouse);
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

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _libraryContext.Book
                .Include(e => e.Copies)
                .FirstOrDefaultAsync(l => l.BookId == id);

            if (book == default) return NotFound();

            _libraryContext.BookCopy.RemoveRange(book.Copies);
            _libraryContext.Book.Remove(book);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("copy/{id}")]
        public async Task<ActionResult> DeleteBookCopy(int id)
        {
            var bookCopy = await _libraryContext.BookCopy.FirstOrDefaultAsync(l => l.BookCopyId == id);

            if (bookCopy == default) return NotFound();

            _libraryContext.BookCopy.Remove(bookCopy);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/copies")]
        public async Task<ActionResult<IEnumerable<BookCopy>>> GetBookCopies(int id)
        {
            var book = await _libraryContext.Book
                .Include(b => b.Copies)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null) return NotFound();

            return Ok(book.Copies.Select(copy => new
            {
                copy.BookCopyId,
                copy.BookId
            }));
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
                return -1;
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
                return -1;
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
                return -1;
            }
        }

        public async Task<int> GetPublishingHouseIdByNameAsync(string phName)
        {
            var house = await _libraryContext.PublishingHouse
                .FirstOrDefaultAsync(a => a.Name == phName);

            if (house != null)
            {
                return house.PublishingHouseId;
            }
            else
            {
                return -1;
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
                return "";
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
                return "";
            }
        }


        [HttpGet("parameters")]
        public async Task<ActionResult<Book>> GetBookByParamethers([FromBody]BookCheckDTO bookDTO)
        {
            var query = _libraryContext.Book
               .Include(b => b.BookAuthors)
               .ThenInclude(ab => ab.Author)
               .Include(b => b.PublishingHouse)
               .Where(b => b.Title == bookDTO.Title)
               .AsQueryable();

            if (!string.IsNullOrEmpty(bookDTO.PublishingHouse))
            {
                query = query.Where(b => b.PublishingHouse.Name == bookDTO.PublishingHouse);
            }

            var book = await query.Select(b => new BookDTO
            {
                Book = b,
                AuthorNames = b.BookAuthors.Select(ab => ab.Author.Pseudonym).ToList(),
                Language = _libraryContext.Language.Where(l => l.LanguageId == b.LanguageId).Select(l => l.Name).FirstOrDefault(),
                Category = _libraryContext.Category.Where(c => c.CategoryId == b.CategoryId).Select(c => c.Name).FirstOrDefault(),
                PublishingHouse = b.PublishingHouse.Name
            }).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        private bool BookExists(int id)
        {
            return _libraryContext.Book.Any(e => e.BookId == id);
        }
    }
}
