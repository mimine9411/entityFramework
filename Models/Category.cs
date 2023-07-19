public class Category
{
    public int Id { get; set; }

    public required string Name {get; set; }

    public virtual List<Hotel> Hotels {get; set; } = new List<Hotel>();

    public override string ToString()
    {
        return Name;
    }

    public Category()
    {

    }
    
    public Category(string name)
    {
        Name = name;
    }
}