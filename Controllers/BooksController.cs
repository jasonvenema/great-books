using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatBooks.Interfaces;
using GreatBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace GreatBooks.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IOpenLibraryService _library;

        public BooksController(IBookRepository repository, IOpenLibraryService library)
        {
            _repository = repository;
            _library = library;
        }

        [HttpPost("Add/{isbn}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAsync(string isbn)
        {
            if (ModelState.IsValid)
            {
                var book = await _library.GetBookData(isbn);

                if (book != null)
                {
                    await _repository.AddItemAsync(book);
                }

                return Ok(book);
            }

            //return BadRequest();
            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var books = await _repository.GetItemsAsync("SELECT TOP 10 * FROM Books");

                if (books != null)
                {
                    return Ok(books);
                }

                return Ok();
            }
            catch (Exception)
            {
                //return BadRequest();
                return Ok();
            }
        }
    }
}
