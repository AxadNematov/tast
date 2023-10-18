using Newtonsoft.Json;

namespace UserDomain.ViewModels.PhotosServiceModels;

public class DeletePhotoRequest
{
    [JsonIgnore]
    public int UserId { get; set; }
    
    public int PhotoId { get; set; }
}