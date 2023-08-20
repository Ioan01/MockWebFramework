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
        private int count;

        [HttpGet("get/bookid")]
        public string Get([FromRoute]int bookId, [FromQuery]int sort)
        {
            return $"Book {bookId} sort {sort}";
        }

        [HttpGet]
        public string Get([FromQuery] string mata)
        {
            return $"Ma-ta e {mata}";
        }

        [HttpPost]
        public int Count([FromBody] int count)
        {
            this.count += count;

            return this.count;
        }

        [HttpRoute("PUT")]
        public int Count()
        {
            return 1;
        }



    }
}
