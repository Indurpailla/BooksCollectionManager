using System.Net.NetworkInformation;
using BooksCollectionManager.Services.Models;
using BooksCollectionManager.Services.ServiceInterfaces;
using Newtonsoft.Json;

namespace BooksCollectionManager.Services.Services;

public class BookStatusService : IBookStatusService
{
    //path to the json data file
    private const string _SDataFilePath = "StatusesData.json";

    public BookStatus? Insert(BookStatus bookStatus)
    {
        List<BookStatus> bookStatuses = LoadData();
        BookStatus _newStatus = new BookStatus();
        _newStatus.StatusId = bookStatuses.Any() ? bookStatuses.Max(b => b.StatusId) + 1 : 1;
        _newStatus.BookId = bookStatus.BookId;
        _newStatus.UserId = bookStatus.UserId;
        _newStatus.StatusValue = bookStatus.StatusValue;
        _newStatus.CreatedAt = bookStatus.CreatedAt;

        bookStatuses.Add(_newStatus);
        
        // Writing JSON to the file
        System.IO.File.WriteAllText(_SDataFilePath, JsonConvert.SerializeObject(bookStatuses));
        return bookStatus;
    }

    private List<BookStatus> LoadData()
    {
        // reading the contents of the JSON file
        var json = System.IO.File.ReadAllText(_SDataFilePath);
        // deserialize the JSON into a list of books, or return an empty list if the deserialization fails
        return JsonConvert.DeserializeObject<List<BookStatus>>(json) ?? new List<BookStatus>();

    }
}
