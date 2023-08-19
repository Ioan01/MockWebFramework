using MockWebFramework.Controller.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Controllers
{
    internal class Books
    {
        [HttpGet("get/bookid")]
        public string Get([FromRoute]string bookId, [FromQuery]string sort)
        {
            return $"Book {bookId} sort {sort}";
        }

    }
}
