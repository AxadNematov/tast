using System.Text.Json.Serialization;

namespace UserDomain.ViewModels;

public class DeleteFriendRequest
{
    [JsonIgnore]
    public int UserId { get; set; }
    public string FriendUserName { get; set; }
}