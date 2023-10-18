using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserDomain.ViewModels;

public class AddFriendRequest
{
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public int UserId { get; set; }
    
    [Required]
    public string FriendUserName { get; set; }
}