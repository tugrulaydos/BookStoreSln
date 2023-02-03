using BookStore.Exceptions;
using BookStore.Filter;
using BookStore.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Runtime.CompilerServices;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    

    BookService BookService = new BookService();
      


    [HttpGet]
    public List<Book> GetBooks()
    {        
        var books = BookService.GetAll();
        return books;
       
    }
    [HttpGet("list")]
    public List<Book> GetBookList([FromQuery]BookFilter f1)   
    {
        var books = BookService.GetAll(f1);

        return books;
    }



    [HttpGet("{Id}")]
    public Book GetById(int Id)
    {
        
        var book = BookService.GetById(Id);
        return book;       
    }

    [HttpPost]
    public IActionResult AddBook([FromQuery]Book newBook)
    {     

        BookService.AddBook(newBook);

        return Ok();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteBook(int Id) 
    {        

        BookService.DeleteBook(Id);

        return Ok();    

    }

    [HttpPut("{Id}")]
    public IActionResult UpdateBook(int Id,[FromBody]Book UpdatedBook) 
    {
        

        BookService.UpdateBook(Id, UpdatedBook);
       

        return Ok();       

    }

    

    [HttpPatch]
    public IActionResult Patch(int id, JsonPatchDocument<Book> patchDocument) 
    {

        if (patchDocument == null) { return BadRequest(); }

       
        var book = BookService.GetById(id);

        if (book == null)
        {
            throw new NotFoundException("Not Found");
        }

        patchDocument.ApplyTo(book, ModelState);

        if (ModelState.IsValid) return Ok(book);

        return BadRequest(ModelState);       


    }


   
}