using GeneralDomain.Responses;
using UserDomain.ViewModels;
using UserDomain.ViewModels.PhotosServiceModels;

namespace UserApplication.Services;

public interface IPhotosService
{
    Task<ApiResponse<string>> AddPhotos(AddPhotosRequest request);
    Task<ApiResponse<bool>> DeletePhoto(DeletePhotoRequest request);

    Task<ApiResponse<GetPhotosResponse>> GetPhotos(GetPhotosRequest request);
}