using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
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

    [HttpGet("list")]
    public List<Book> GetBookList([FromQuery]string name)   //Listeleme
    {
        var books = BookList.Where(x => x.Title == name).ToList();
       
       return books;
    }

    [HttpGet("Books")]
    public List<Book> GetSortedList([FromQuery] int sort)
    {

        var books = BookList.OrderByDescending(x => x.Id).ToList();

        return books;
    }
  


    [HttpGet]
    public List<Book> GetBooks()
    {
        var books = BookList.OrderBy(x => x.Id).ToList();
        return books;
    }    



    [HttpGet("{Id}")]
    public Book GetById(int Id)
    {
        var book = BookList.FirstOrDefault(x=>x.Id==Id);
        return book;       
    }

    [HttpPost]
    public IActionResult AddBook([FromBody]Book newBook)
    {        
        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        if (book is not null)
        {
            return BadRequest();
        }

        BookList.Add(newBook);


        return Ok();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteBook(int Id) 
    {
        var book = BookList.SingleOrDefault(x=>x.Id==Id);

        if (book == null)
        {
            return BadRequest();
        }

        BookList.Remove(book);

        return Ok();    

    }

    [HttpPut("{Id}")]
    public IActionResult UpdateBook(int Id,[FromBody]Book UpdatedBook) //Model Binding Body
    {
        var book = BookList.SingleOrDefault(x => x.Id== Id);
        if (book is null) { return BadRequest(); }

        book.Id = UpdatedBook.Id!=default?UpdatedBook.Id:book.Id;
        book.Title = UpdatedBook.Title!=default?UpdatedBook.Title:book.Title;
       
        book.PublicationDate = UpdatedBook.PublicationDate!=default?UpdatedBook.PublicationDate:book.PublicationDate;
        book.Summary = UpdatedBook.Summary != default ? UpdatedBook.Summary : book.Summary;
       

        return Ok();       

    }

    [HttpPut]
    public IActionResult UpdateBookBody(int Id, [FromQuery]Book UpdatedBook)  //Model Binding Query 
    {
        var book = BookList.SingleOrDefault(x => x.Id== Id);

        if (book is null) { return BadRequest(); }

        book.Id = UpdatedBook.Id != default ? UpdatedBook.Id : book.Id;
        book.Title = UpdatedBook.Title != default ? UpdatedBook.Title : book.Title;
        book.PublicationDate = UpdatedBook.PublicationDate != default ? UpdatedBook.PublicationDate : book.PublicationDate;
        book.Summary = UpdatedBook.Summary != default ? UpdatedBook.Summary : book.Summary;
        return Ok();
    }


    [HttpPatch]
    public IActionResult Patch(int id, JsonPatchDocument<Book> patchDocument) 
    {

        if (patchDocument == null) { return BadRequest(); }

        var book = BookList.SingleOrDefault(x=>x.Id== id);

        if(book == null) 
        {
            return NotFound();
        }

        patchDocument.ApplyTo(book, ModelState);

        if (ModelState.IsValid) return Ok(book);

        return BadRequest(ModelState);
    }


   
}