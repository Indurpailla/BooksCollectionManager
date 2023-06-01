using System;
using BooksCollectionManager.Controllers;
using BooksCollectionManager.Services.Models;
using BooksCollectionManager.Services.ServiceInterfaces;
using BooksCollectionManager.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BooksCollectionManagerTest
{
        [TestClass]
        public class BooksServiceTest
        {

        private readonly IBookService _bookService;

        public BooksServiceTest()

        {
            _bookService = new BookService();
        }

        [TestMethod]
        public void GetAllBooksTest()
        {
           
            var result = _bookService.GetAllBooks();
            Assert.AreEqual(_bookService.GetAllBooks().Count(), result.Count());
        }

        [TestMethod]
        public void GetBookByIdTest()
        {
            var bookId = 1;

            var result = _bookService.GetBookById(bookId);

          
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Book));
            Assert.AreEqual(bookId, result.BookId);
            Assert.AreEqual(StatusEnum.Available, result.Status);
        }

        [TestMethod]
        public void CreateBookTest()
        {

            //Arange
            Book newBook = new Book
            {
                Title = "Test",
                Author = "Author",
                Genre = "Genre",
                PublicationYear = 2022,
                Status = StatusEnum.Available 
            };

            // Act
            Book? result = _bookService.Upsert(newBook);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newBook.Title, result.Title);
            Assert.AreEqual(newBook.Author, result.Author);
            Assert.AreEqual(newBook.Genre, result.Genre);
            Assert.AreEqual(newBook.PublicationYear, result.PublicationYear);
            Assert.AreEqual(StatusEnum.Available, result.Status);
        }

        [TestMethod]
        public void UpdateBookTest()
        {

            //Arrange
            var existingBookId = 1;
            Book updatedBook = new Book
            {
                Title = "Test Title",
                Author = "Author",
                Genre = "Genre",
                PublicationYear = 2023,
                Status = StatusEnum.Rented
            };

            // Act
            Book? result = _bookService.Upsert(updatedBook, existingBookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedBook.Title, result.Title);
            Assert.AreEqual(updatedBook.Author, result.Author);
            Assert.AreEqual(updatedBook.Genre, result.Genre);
            Assert.AreEqual(updatedBook.PublicationYear, result.PublicationYear);
            Assert.AreEqual(StatusEnum.Rented, result.Status);
        }

        [TestMethod]
        public void GetBookWithInValidIDTest()
        {
            var bookId = 999;

            Book? result = _bookService.GetBookById(bookId);

            Assert.IsNull(result);
           
        }
        
    }
}

