using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class Books : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {

            return Ok(new List<Book>()
            {
                new Book(){Id = 1,Name = "Book1",Author = "Author1" },
                new Book(){Id = 2,Name = "Book2",Author = "Author2" },
                new Book(){Id = 3,Name = "Book3",Author = "Author3" },
            });
        }

        [HttpGet("{Id}")]
        public ActionResult<Book> GetBook(int Id)
        {
            var books = new List<Book>() {
                new Book(){Id = 1, Name = "Book1", Author = "Author1" },
                new Book(){Id = 2, Name = "Book2", Author = "Author2" },
                new Book(){Id = 3, Name = "Book3", Author = "Author3" }
            };

            var book = books.FirstOrDefault(b => b.Id == Id);

            if (book == null)
            {
                return NotFound($"{Id} Book not found");
            }

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] Book newBook)
        {
            if (newBook == null || string.IsNullOrWhiteSpace(newBook.Name) || string.IsNullOrWhiteSpace(newBook.Author))
            {
                return BadRequest("Book information is incomplete.");
            }

            var books = new List<Book>() {
        new Book(){Id = 1, Name = "Book1", Author = "Author1" },
        new Book(){Id = 2, Name = "Book2", Author = "Author2" },
        new Book(){Id = 3, Name = "Book3", Author = "Author3" }
    };

            newBook.Id = books.Max(b => b.Id) + 1;

            books.Add(newBook);

            return CreatedAtAction(nameof(GetBook), new { Id = newBook.Id }, newBook);
        }


    }
}
