using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public AppContext (DbContextOptions<AppContext> options): base(options) {

    }
    public required DbSet<User> Users { get; set; }
    public required DbSet<Hotel> Hotels { get; set; }
    public required DbSet<Room> Rooms { get; set; }
    public required DbSet<Category> Categories { get; set; }
}