using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace UserDomain.ViewModels.PhotosServiceModels;

public class GetPhotosRequest
{
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public int UserId { get; set; }
    
    [AllowNull]
    public bool WithFriendPhotos { get; set; }
}