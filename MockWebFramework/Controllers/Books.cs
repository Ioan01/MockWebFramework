using MockWebFramework.Controller.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Controller.Attributes.Endpoint;
using MockWebFramework.Controller.Attributes.From;
using MockWebFramework.Models;

namespace MockWebFramework.Controllers
{
    internal class Books
    {
        private int count;

        [HttpGet("get/bookid")]
        public Book Get([FromRoute]string bookId, [FromQuery]int? sort)
        {
            return new Book()
            {
                Name = bookId,
                Author = "dasdas",
                Price = 213,
                Year = sort
            };

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

        [HttpPost]
        public Book GetById([FromBody]Book id)
        {
            return new Book();
        }

        [HttpPost]
        public Book Add([FromBody]string author,[FromBody] int year)
        {
            return new Book()
            {
                Author = author,
                Year = year
            };
        }



    }
}
