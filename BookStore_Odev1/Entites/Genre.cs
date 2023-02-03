using System.ComponentModel.DataAnnotations;

namespace BookStore;

public class Genre
{
    [Required]
    public int Id{get;set;}

    public string Name{get;set;}
}