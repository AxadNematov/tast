using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("user_friend", Schema ="user")]
public class UserFriends
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Column("friend_id")]
    [ForeignKey("UserFriend")]
    public int FriendId { get; set; }
    public User UserFriend { get; set; }
}