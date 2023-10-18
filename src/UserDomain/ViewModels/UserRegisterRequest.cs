using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UserDomain.ViewModels;

public class UserRegisterRequest
{
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [AllowNull]
    public string? Email { get; set; }
}