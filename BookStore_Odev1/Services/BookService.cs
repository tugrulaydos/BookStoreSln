using BookStore.Exceptions;
using BookStore.Filter;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStore.Services
{
    public class BookService
    {


        public static List<Book> BookList = new List<Book>()
        {
        new Book()
        {
            Id=1,Title="Lord Of The Rings",PublicationDate = new DateTime(1954),
            Genres = new List<Genre>{ new Genre() {Id=1,Name = "Sceince Fiction"}, new Genre() {Id = 2, Name = "Novel"}}
        },

        new Book()
        {
            Id=2,Title="Harry Potter",PublicationDate = new DateTime(1997),
            Genres = new List<Genre>{ new Genre() {Id=1,Name = "Sceince Fiction"}, new Genre() {Id=2, Name = "Novel" }}
        },

        new Book()
        {
            Id=3, Title="Silence Of The Lambs", PublicationDate = new DateTime(2014),
            Genres = new List<Genre>{ new Genre() {Id=3,Name = "Crime"}, new Genre() {Id=2, Name = "Novel" }}
        },

        new Book()
        {
            Id=4,Title="Pardaillans", PublicationDate = new DateTime(1991),
            Genres = new List <Genre> {new Genre() {Id=4, Name = "Historical Novel" }, new Genre() {Id=5,Name = "Epic"} }
        },

        new Book()
        {
            Id=5,Title="Man In Searh Of Meaning", PublicationDate = new DateTime(1946),
            Genres = new List <Genre> { new Genre() {Id=6, Name = "Self-Help" }, new Genre() {Id=7, Name = "Biography"}}
        }

        };


        public List<Book> GetAll(BookFilter filter = null) 
        {
            var books = BookList;
            
            if (filter!=null) 
            {
                if (filter.Title != null)
                {
                    books = books.Where(x => x.Title == filter.Title).ToList();

                }                  

            }        
            
            
            return books.OrderBy(x => x.Id).ToList();

        }

        public Book GetById(int Id)
        {
            var book = BookList.FirstOrDefault(x => x.Id == Id);
            return book;

        }

        public void AddBook(Book newBook)
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if (book is null) //This book is not available
            {
                BookList.Add(newBook);                
            }
            else //This book is already available (Error)
            {
                throw new BadRequestException("This Book is Already Avaible");
            }

        }

        public void DeleteBook(int id)
        {
            var book = GetById(id);

            if (book != null)
            {
                BookList.Remove(book);
            }
            else //Book is not found
            {
                throw new BadRequestException("Book is not found.");
                
            }

        }

        public void UpdateBook(int id, Book UpdatedBook)
        {
            var book = GetById(id);
            if(book is not null) 
            {
                book.Id = UpdatedBook.Id != default ? UpdatedBook.Id : book.Id;
                book.Title = UpdatedBook.Title != default ? UpdatedBook.Title : book.Title;

                book.PublicationDate = UpdatedBook.PublicationDate != default ? UpdatedBook.PublicationDate : book.PublicationDate;
                book.Summary = UpdatedBook.Summary != default ? UpdatedBook.Summary : book.Summary;

            }
            else
            {
                throw new BadRequestException("Book is not found.");
            }

        }

       

        


    }
}
