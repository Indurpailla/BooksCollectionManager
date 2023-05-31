using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksCollectionManager.Services;
using BooksCollectionManager.Services.Models;
using BooksCollectionManager.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BooksCollectionManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IBookStatusService _bookStatusService;

        public StatusController(IBookStatusService bookStatusService)
        {
            _bookStatusService = bookStatusService;
        }

        [HttpPost]
        public ActionResult<BookStatus> CreateStatus(BookStatus status)
        {
            BookStatus? insertedBookStatus = _bookStatusService.Insert(status);
            if (insertedBookStatus == null)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return StatusCode(StatusCodes.Status400BadRequest, new BookStatus());
            }
            return StatusCode(StatusCodes.Status201Created, insertedBookStatus);
        }

    }
}
