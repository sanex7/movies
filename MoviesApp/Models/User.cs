using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApp.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public List<Title> Titles { get; set; }
}
