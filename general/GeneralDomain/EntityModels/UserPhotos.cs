using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("user_photos", Schema ="user")]
public class UserPhotos
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Column("file_name")]
    public string FileName { get; set; }
    
    [Column("add_date")]
    public DateTime AddDate { get; set; }
}