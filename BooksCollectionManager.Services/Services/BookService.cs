using System;
using System.Net.NetworkInformation;
using BooksCollectionManager.Services.Models;
using BooksCollectionManager.Services.ServiceInterfaces;
using Newtonsoft.Json;

namespace BooksCollectionManager.Services.Services
{
	public class BookService : IBookService
    {
        //path to the json data file
        private const string DataFilePath = "BooksData.json";
        private const string _SDataFilePath = "StatusesData.json";


        public BookService()
		{
		}

        public bool DeleteBook(int id)
        {
            List<Book> books = LoadData();
            Book? book = books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return false;
            }
            books.Remove(book);
            // writing the JSON to the file
            System.IO.File.WriteAllText(DataFilePath, JsonConvert.SerializeObject(books));
            return true;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = LoadData();

            //Load the statuses
            var statuses = ReadStatusesFromJson();

            //Get the most recent status of all the books
            foreach (var book in books)
            {
                book.Status = getMostResentStatus(book.BookId, statuses);
            }

            // returning the list of books
            return books;
        }

        public Book? GetBookById(int id)
        {
            List<Book> books = LoadData();
            // finding book with the specified ID
            Book? book = books.FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return null;
            }

            var statuses = ReadStatusesFromJson();
            book.Status = getMostResentStatus(id, statuses);

            return book;
        }

        //Lookup the most recent status of the book in statuses and return
        //If there is no status entry in the status for the current book, return as "Available"
        private static StatusEnum getMostResentStatus(int bookId, List<BookStatus> statuses)
        {

            // Get the most recent status
            var mostRecentStatus = statuses.Where(s => s.BookId == bookId).OrderByDescending(s => s.CreatedAt).FirstOrDefault();
            if (mostRecentStatus != null)
            {
                return mostRecentStatus.StatusValue;
            }else
            {
                return StatusEnum.Available;
            }
        }

        public Book? Upsert(Book upsertBook, int id = 0)
        {
            List<Book> books = LoadData();
            Book? book = new Book();
            if (id != 0)
            {
                book = books.FirstOrDefault(b => b.BookId == id);
                if (book == null)
                {
                    return null;
                }

                //updaing book details with new values
                book.Title = upsertBook.Title;
                book.Author = upsertBook.Author;
                book.Genre = upsertBook.Genre;
                book.PublicationYear = upsertBook.PublicationYear;
                //I'm retrcting status update through book object to get the latest status from status object
                //book.Status = upsertBook.Status;
               
                // writing the JSON to the file
                System.IO.File.WriteAllText(DataFilePath, JsonConvert.SerializeObject(books));
            }
            else
            {
                //creating new book
                book.BookId = books.Any() ? books.Max(b => b.BookId) + 1 : 1;
                book.Title = upsertBook.Title;
                book.Author = upsertBook.Author;
                book.Genre = upsertBook.Genre;
                book.PublicationYear = upsertBook.PublicationYear;
                //since this is a new book the status will be set to available
                book.Status = StatusEnum.Available;

                books.Add(book);
            }

            // writing the JSON to the file
            System.IO.File.WriteAllText(DataFilePath, JsonConvert.SerializeObject(books));
            return book;
        }


        private List<Book> LoadData()
        {
            // reading the contents of the JSON file
            var json = System.IO.File.ReadAllText(DataFilePath);
            // deserialize the JSON into a list of books, or return an empty list if the deserialization fails
            return JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();

        }
        private List<BookStatus> ReadStatusesFromJson()
        {
            var json = File.ReadAllText(_SDataFilePath);
            return JsonConvert.DeserializeObject<List<BookStatus>>(json) ?? new List<BookStatus>();
        }
    }
}

