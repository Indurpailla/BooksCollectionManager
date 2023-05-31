using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace BooksCollectionManager.Services.Models
{
    public enum StatusEnum
	{
        Available,
        Rented
    }
}
