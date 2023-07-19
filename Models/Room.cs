public class Room
{
    public int Id { get; set; }

    public required string Name {get; set; }

    public required string Number {get; set;}

    public required string Type {get; set;}

    public required string Floor {get; set;}

    public required int Capacity {get; set;}

    public required float Price {get; set;}

    public bool Available {get; set;} = true;

    public int HotelId {get; set;}

    public virtual Hotel Hotel {get; set; }


    public Room()
    {
        
    }
    
    public Room(string name, string type, string number, string floor, int capacity, float price, bool available = true)
    {
        Name = name;
        Number = number;
        Type = type;
        Floor = floor;
        Capacity = capacity;
        Price = price;
        Available = available;
    }
}