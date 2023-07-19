using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Hotel
{
    public int Id { get; set; }

    public required string Name {get; set; }

    public required string Location {get; set; }
    
    public int CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    [Range(2, int.MaxValue, ErrorMessage = "The value must be greater than 1.")]
    public required int Capacity  { get; set; }

    public virtual List<Review> Reviews { get; set; } = new List<Review>();

    public virtual List<Room> Rooms { get; set; } = new List<Room>();



    public Hotel()
    {

    }
    
    public Hotel(string name, string location, Category category)
    {
        Name = name;
        Location = location;
        Category = category;
    }
}