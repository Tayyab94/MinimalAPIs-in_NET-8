using Net8_MinimalAPIs.Models;

namespace Net8_MinimalAPIs.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>
            {
                new()
                {
                    Id = 1,
                    Title = "Dependency Injection in .NET",
                    Author = "Mark Seemann"
                },
                new()
                {
                    Id = 2,
                    Title = "C# in Depth",
                    Author = "Jon Skeet"
                },
                new()
                {
                    Id = 3,
                    Title = "Programming Entity Framework",
                    Author = "Julia Lerman"
                },
                new()
                {
                    Id = 4,
                    Title = "Programming WCF Services",
                    Author = "Juval Lowy and Michael Montgomery"
                }
            };
        }
        public Book GetBook(int id)
        {

            return _books.FirstOrDefault(x => x.Id == id);
        }

        public List<Book> GetBooks()
        {
            return _books;
        }
    }
}
