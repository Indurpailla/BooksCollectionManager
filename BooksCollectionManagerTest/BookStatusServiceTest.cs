using System;
using System.Net.NetworkInformation;
using BooksCollectionManager.Controllers;
using BooksCollectionManager.Services.Models;
using BooksCollectionManager.Services.ServiceInterfaces;
using BooksCollectionManager.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksCollectionManagerTest

{
    [TestClass]
    public class BookStatusServiceTest
	{
        private readonly IBookStatusService _bookStatusService;
        public BookStatusServiceTest()
		{
            _bookStatusService = new BookStatusService();
        }
    
        [TestMethod]
        public void CreateStatusTest()
        {
            //Arrange
            BookStatus bookStatus = new BookStatus
            {
                StatusId = 1,
                BookId = 1,
                UserId = 1,
                StatusValue = StatusEnum.Available,
                CreatedAt = DateTime.Now
            };

            // Act
            BookStatus? result = _bookStatusService.Insert(bookStatus);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookStatus.StatusId, result.StatusId);
            Assert.AreEqual(bookStatus.BookId, result.BookId);
            Assert.AreEqual(bookStatus.UserId, result.UserId);
            Assert.AreEqual(bookStatus.StatusValue, result.StatusValue);
            Assert.AreEqual(bookStatus.CreatedAt, result.CreatedAt);

        }

    }
}

