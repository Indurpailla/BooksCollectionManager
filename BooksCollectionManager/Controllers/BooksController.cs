using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksCollectionManager.Services.Models;
using BooksCollectionManager.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//This controller manages Books Collection
namespace BooksCollectionManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookService _booksService;

        public BooksController(IBookService bookService)
        {
            _booksService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            List<Book> books = _booksService.GetAllBooks();
            if (books == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new List<Book>());
            }
            return StatusCode(StatusCodes.Status200OK, books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            Book? book = _booksService.GetBookById(id);
            if (book == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Book());
            }
            return StatusCode(StatusCodes.Status200OK, book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            Book? insertedBook = _booksService.Upsert(book);
            if (insertedBook == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return StatusCode(StatusCodes.Status400BadRequest, new Book());
            }
            return StatusCode(StatusCodes.Status201Created, insertedBook);
        }

        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, Book updatedBook)
        {
            Book? book = _booksService.Upsert(updatedBook, id);
      
            if (book == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Book());
            }
            return StatusCode(StatusCodes.Status202Accepted, book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            bool deleteStatus = _booksService.DeleteBook(id);
            if (deleteStatus == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest, deleteStatus);
            }
            return StatusCode(StatusCodes.Status200OK, deleteStatus);
        }

    }
}
