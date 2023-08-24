# Macabee(Mock) Web Framework

A simple HTTP 1.1 server + ASP.NET-like framework to create simple Web APIs

## Overview
- Features a simple HTTP server created over the .NET Socket library
- Custom HTTP packet parsing of header and body portions
- HTTP request routing
- Dependency injection for services and controllers
- (Planned) Filter attributes for endpoints

## Usage
- Instantiate the web server via 
``
var web = new WebServer();
``

- Services can be quickly added from a namespace (by default Services, meaning all service classes in the folder and sub-folders in Services will be added as services)
``        web.Services.RegisterServices();
``
- Otherwise, Services can be manually registered via 
`` web.Services.AddPrototype<Service>();
``
or
``
web.Services.AddSingleton<Service>();
``

- Controllers can be added from a namespace just like Services. By default, that namespace is Controllers/*
``
        web.Controllers.RegisterControllers();
``
- After services and controllers are registered, start the server
``
await web.Start();
``


# Examples
- Sample controller

```csharp
class Books
{
        // Service Dependencies
        private readonly LibraryService _service;
        
        // constructor for dependencies
        public Books(LibraryService service)
        {
            _service = service;
        }
        
        // route string is optional unless route parameters are used
        [HttpGet("/get/all")]
        public List<Book> GetBooks([FromQuery] int? sort, [FromQuery] string? test)
        {
            if (_service.GetBooks(sort).Count == 0)
                // 
                throw new HttpException(HttpStatusCode.NotFound,"no books"); 
                // or throw new NotFoundException("no books");
            return _service.GetBooks(sort);
        }
        
        // bookid is a fromRoute parameter, it needs to appear in the route
        [HttpGet("/get/bookid")]
        public Book? Get([FromRoute]string bookId)
        {
            return _service.GetBooks(sort).FirstOrDefault(b => b.Name == bookId);
        }
        
        // books is a json array in the json body of the request
        [HttpPost]
        public IEnumerable<Book> AddMultiple([FromBody] IEnumerable<Book> books,[FromBody]int number)
        {
            return _service.AddBooks(books);
        }
        ...
}
```
- Sample service 
```csharp
public class LibraryService
{
        // service dependencies
        private readonly BookService _service;
        
        // dependency constructor
        public LibraryService(BookService service)
        {
            _service = service;
        ...
}
```






