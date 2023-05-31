using System;
using BooksCollectionManager.Services.Models;

namespace BooksCollectionManager.Services.ServiceInterfaces
{
	public interface IBookStatusService
	{
        BookStatus? Insert(BookStatus bookStatus);
    }
}

