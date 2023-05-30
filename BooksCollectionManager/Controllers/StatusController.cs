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
    public class StatusController : ControllerBase
    {
        private const string DataFilePath = "data.json";

        [HttpPost]
        public ActionResult<BookStatus> CreateStatus(BookStatus status)
        {
            var books = LoadData();
            var book = books.FirstOrDefault(b => b.BookId == status.BookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            book.Status = status;
            SaveData(books);

            return CreatedAtAction(nameof(BooksController.GetBook), new { id = book.BookId }, book);
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

    }
}
