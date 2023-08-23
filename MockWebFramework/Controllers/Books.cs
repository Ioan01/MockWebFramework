using MockWebFramework.Controller.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Controller.Attributes.Endpoint;
using MockWebFramework.Controller.Attributes.From;
using MockWebFramework.Http.HttpExceptions;
using MockWebFramework.Models;
using MockWebFramework.Services.Books;

namespace MockWebFramework.Controllers
{
    internal class Books
    {
        private readonly LibraryService _service;
        

        public Books(LibraryService service)
        {
            _service = service;
        }

        [HttpGet("/get/all")]
        public List<Book> GetBooks([FromQuery] int? sort, [FromQuery] string? test)
        {
            return _service.GetBooks(sort);
        }

        [HttpGet("/get/bookid")]
        public Book? Get([FromRoute]string bookId, [FromQuery]int? sort)
        {
            var book = _service.GetBooks(sort).FirstOrDefault(b => b.Name == bookId);
            if (book == null)
            {
                return null;
            }

            return book;
        }

        

        [HttpPost]
        public Book Add([FromBody]Book book)
        {
            return _service.AddBook(book);
        }

        [HttpPost]
        public IEnumerable<string> AddMultiple([FromBody] IEnumerable<string> books)
        {

            return books;
;            
        }

        




    }
}
