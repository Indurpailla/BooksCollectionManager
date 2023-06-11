# BooksCollectionManager
The API will allow users to manage a collection of books. Please upload your solution to a public code repository (GIthub, Gitlab, whichever you prefer).

Requirements:

Use a local JSON file called 'data.json' as your data store. Two objects will be stored on the file with the following properties:

 
BOOKS:

BookId (int): Unique identifier for the book.
Title (string): Title of the book.
Author (string): Author of the book.
Genre (string): Genre of the book.
PublicationYear (int): Year of publication of the book.
Status (enum): Indicates the status of the book (e.g., "Available" or "Rented").
 

 

STATUSES:

StatusId (int): Unique identifier for the status entry.
BookId (int): ID of the book associated with the status.
UserId (int): ID of the user who rented the book.
Status (enum): Indicates the status of the book (e.g., "Available" or "Rented").
CreatedAt (datetime): Timestamp of the creation of the status event
 

Example of BooksData.JSON:

[{
id: 11231232,
title: 'A Game of Thrones', 
author: 'George RR Martin',
genre: 'Fantasy', 
publication_year: 1996,
Status : 'Available'
}]

Example of StatusesData.JSON:

[{
id: 1234234, 
book_id: 11231232, 
user id: 123, 
status: Rented',
createdAt: TIMESTAMP
}]



The API should support the following operations:


Books:

GET /api/books: Retrieve a list of all the books.
GET /api/books/{id}: Retrieve the details of a specific book by its ID.
POST /api/books: Create a new book entry.
PUT /api/books/{id}: Update the details of an existing book.
DELETE /api/books/{id}: Remove a book from the collection.
 

Status:

POST /api/statuses: Create a new status entry.
 

The "Status" class and endpoint allow you to manage the availability and rental information of books.

By associating a book with its corresponding user and status, you can track whether a book is available or rented out through the API.

The Status object will determine the state of the Book object when retrieving a book by retrieving the most recent status object and returning the appropriate status.

The API should handle the CRUD operations for both books and statuses, providing users with the ability to manage the collection of books and their respective status entries.

# We can use InMemory Cache for to improve the efficiency of data retrieval. I can make those changes if required.
