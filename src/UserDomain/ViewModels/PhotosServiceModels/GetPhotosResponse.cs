namespace UserDomain.ViewModels.PhotosServiceModels;

public class GetPhotosResponse
{
    public List<Photo> OwnPhotos { get; set; }
    
    public List<Photo> FriendPhotos { get; set; }
}

public class Photo
{
    public int Id { get; set; }
    
    public string UserName { get; set; }
    public string Path { get; set; }
    public DateTime CreatDateTime { get; set; }
}