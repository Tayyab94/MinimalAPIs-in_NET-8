namespace Net8_MinimalAPIs.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
    }

    public interface IBookService
    {
        List<Book> GetBooks();

        Book GetBook(int id);
    }
}
