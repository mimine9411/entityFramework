public class User
{
    public int Id { get; set; }

    public required string Username {get; set; }

    public required string Password {get; set; }
    
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? PhoneNumber { get; set; }


    public User()
    {

    }
    
    public User(string username, string password, string firstName, string lastName, DateTime? birthDate, string phoneNumber)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
    }
}