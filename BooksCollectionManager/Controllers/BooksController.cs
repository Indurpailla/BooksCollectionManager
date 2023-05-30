using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksCollectionManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BooksCollectionManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //path to the json data file
        private const string DataFilePath = "data.json";

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = LoadData();
            return books.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var books = LoadData();
            var book = books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            var books = LoadData();
            book.BookId = GetNextBookId(books);
            books.Add(book);
            SaveData(books);

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var books = LoadData();
            var book = books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.PublicationYear = updatedBook.PublicationYear;
            book.Status = updatedBook.Status;
            SaveData(books);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var books = LoadData();
            var book = books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            books.Remove(book);
            SaveData(books);

            return NoContent();
        }

        private List<Book> LoadData()
        {
            var json = System.IO.File.ReadAllText(DataFilePath);
            return JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();
        }

        private void SaveData(List<Book> books)
        {
            var json = JsonConvert.SerializeObject(books);
            System.IO.File.WriteAllText(DataFilePath, json);
        }

        private int GetNextBookId(List<Book> books)
        {
            return books.Any() ? books.Max(b => b.BookId) + 1 : 1;
        }

    }
}
