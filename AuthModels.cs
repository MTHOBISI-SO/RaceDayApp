using System.ComponentModel.DataAnnotations;

namespace RaceDayWeb.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; } = "";

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = "";
}

public class RegisterViewModel
{
    [Required] public string FirstName { get; set; } = "";
    [Required] public string LastName { get; set; } = "";
    [Required, DataType(DataType.Date)] public DateTime DateOfBirth { get; set; }
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Phone { get; set; } = "";
    [Required] public string Username { get; set; } = "";
    [Required, MinLength(8), DataType(DataType.Password)] public string Password { get; set; } = "";
    [Required, DataType(DataType.Password)] public string ConfirmPassword { get; set; } = "";
    public string Role { get; set; } = "Participant";
}
