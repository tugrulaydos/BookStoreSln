namespace BookStore;

public class Book
{
      

    public int Id { get; set; }

    public string Title{get;set;}
    public string? Summary{get;set;}



    public DateTime PublicationDate{get;set;} = DateTime.Now;

    public List<Genre>? Genres { get; set; }
    

}