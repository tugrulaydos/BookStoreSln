using System.ComponentModel.DataAnnotations;

namespace BookStore;

public class Book
{

    [Required]
    public int Id { get; set; }

    [Required]
    public string Title{get;set;}

    public string? Summary{get;set;}

    public DateTime PublicationDate{get;set;} = DateTime.Now;

    public List<Genre>? Genres { get; set; }
    

}