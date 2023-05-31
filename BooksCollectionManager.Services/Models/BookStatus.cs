using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace BooksCollectionManager.Services.Models
{
	public class BookStatus 
	{
        public int StatusId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public StatusEnum StatusValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

