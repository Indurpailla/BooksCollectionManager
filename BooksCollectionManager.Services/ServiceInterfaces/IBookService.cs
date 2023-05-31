using System;
using BooksCollectionManager.Services.Models;
namespace BooksCollectionManager.Services.ServiceInterfaces
{
	public interface IBookService
	{
		List<Book> GetAllBooks();

		Book? GetBookById(int id);

		Book? Upsert(Book book, int id = 0);

		bool DeleteBook(int id);
	}
}

