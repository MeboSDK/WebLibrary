using System.ComponentModel.DataAnnotations;

namespace WebLibrary.Models.UserModels;

public class LogInViewModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
