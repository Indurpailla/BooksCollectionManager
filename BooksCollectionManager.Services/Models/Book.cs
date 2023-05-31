using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace BooksCollectionManager.Services.Models
{
	public class Book
	{
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int PublicationYear { get; set; }
        public StatusEnum Status { get; set; }
    }
}

