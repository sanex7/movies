using MoviesApp.Models;

public class Title
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
