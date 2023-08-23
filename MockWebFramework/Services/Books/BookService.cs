using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Models;

namespace MockWebFramework.Services.Books
{
    public class BookService
    {
        public Book CreateBook() { return new Book(); }
    }
}
