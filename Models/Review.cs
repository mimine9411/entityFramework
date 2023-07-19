public class Review
{
    public int Id { get; set; }

    public required string Title {get; set; }

    public required string Description {get; set; }

    public virtual required Hotel Hotels {get; set; }


    public Review()
    {

    }
    
    public Review(string title, string description)
    {
        Title = title;
        Description = description;
    }
}