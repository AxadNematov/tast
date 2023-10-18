using System.Net;
using GeneralApplication.Extensions;
using GeneralDomain.Configs;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UserDomain.ViewModels.PhotosServiceModels;

namespace UserApplication.Services;

public class PhotosService:IPhotosService
{
    private readonly DataContext _dbContext;

    public PhotosService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<ApiResponse<string>> AddPhotos(AddPhotosRequest request)
    {
        var fileName = FileState.AddFile(request.File);

        var addModel = new UserPhotos
        {
            UserId = request.UserId,
            FileName = fileName,
            AddDate = DateTime.Now
        };

        _dbContext.UserPhotos.Add(addModel);
        _dbContext.SaveChanges();

        return Configs.FilePath + fileName;
    }

    public async Task<ApiResponse<bool>> DeletePhoto(DeletePhotoRequest request)
    {
        var photo = _dbContext.UserPhotos.FirstOrDefault(p => p.Id == request.PhotoId);
        
        if (photo != null && photo.UserId == request.UserId)
        {
            _dbContext.UserPhotos.Remove(photo);
            _dbContext.SaveChanges();

            return true;
        }
        else
        {
            return new ApiResponseError(HttpStatusCode.NotFound, "photo not found"); 
        }
    }

    public async Task<ApiResponse<GetPhotosResponse>> GetPhotos(GetPhotosRequest request)
    {
        GetPhotosResponse response = new GetPhotosResponse(){OwnPhotos = new List<Photo>(), FriendPhotos = new List<Photo>()};
        
        var user = _dbContext.User.FirstOrDefault(u => u.Id == request.UserId);
        if (user == null)
            return new ApiResponseError(HttpStatusCode.NotFound, "invalid user");

        response.OwnPhotos = await GetUserPhotos(user);

        if (request.WithFriendPhotos)
            response.FriendPhotos = await GetFriendPhotos(user);

        return response;
    }

    public async Task<List<Photo>> GetUserPhotos(User user)
    {
        List<Photo> userPhotosList = new List<Photo>();
        var userPhotos = _dbContext.UserPhotos.Where(p => p.UserId == user.Id).ToList();

        foreach (var photo in userPhotos)
        {
            Photo p = new Photo()
            {
                Id = photo.Id,
                UserName = user.UserName,
                Path = Configs.FilePath + photo.FileName,
                CreatDateTime = photo.AddDate
            };
            
            userPhotosList.Add(p);
        }

        return userPhotosList;
    }
    public async Task<List<Photo>> GetFriendPhotos(User user)
    {
        List<Photo> userPhotosList = new List<Photo>();

        List<int> friendsIdList = new List<int>();
        var userFriends = _dbContext.UserFriends.Where(f => f.FriendId == user.Id).ToList();
        foreach (var u in userFriends)
        {
            friendsIdList.Add(u.UserId);
        }

        var userPhotos = _dbContext.UserPhotos.Where(p => friendsIdList.Any(f => f == p.UserId)).Include(m => m.User)
            .OrderBy(p => p.User.UserName).ToList();

        foreach (var photo in userPhotos)
        {
            Photo p = new Photo()
            {
                Id = photo.Id,
                UserName = photo.User.UserName,
                Path = Configs.FilePath + photo.FileName,
                CreatDateTime = photo.AddDate
            };
            
            userPhotosList.Add(p);
        }

        return userPhotosList;
    }
}