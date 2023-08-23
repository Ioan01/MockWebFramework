using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockWebFramework.Models;

namespace MockWebFramework.Services.Books
{
    public class LibraryService
    {
        private readonly BookService _service;
        private List<Book> books = new ();


        public LibraryService(BookService service)
        {
            _service = service;

            for (int i = 0; i < 10; i++)
            {
                books.Add(new Book()
                {
                    Name = $"Book {i}",
                    Author = $"Author {i}",
                    Price = i,
                    Year = 200 + i
                });
            }
        }

        public Book? AddBook(Book book)
        {
            _service.CreateBook();
            books.Add(book);
            return book;
        }

        public List<Book?> GetBooks(int? sort)
        {
            if (sort == null) sort = 0;
            if (sort == -1) return books.Select(b=>b).Reverse().ToList();

            return books;
        }


    }
}
