using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("user", Schema ="user")]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_name")]
    public string UserName { get; set; }
    
    [Column("passowrd")] 
    public string Password { get; set; }
    
    [Column("email")]
    public string? Email { get; set; }
    
    [Column("refresh_token")]
    public string? RefreshToken { get; set; }
    
    public IReadOnlyCollection<UserPhotos> UserPhotos { get; set; }

}
