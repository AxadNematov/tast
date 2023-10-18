using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Services;
using UserDomain.ViewModels;
using UserDomain.ViewModels.PhotosServiceModels;

namespace UserWebService.Controllers;
[Route("photos/[controller]")]
[ApiController]
public class PhotosController : ControllerBase
{
    
    private readonly IPhotosService _photosService;

    public PhotosController(IPhotosService photosService)
    {
        _photosService = photosService;
    }
    
    
    [Authorize]
    [HttpGet("GetPhoto")]
    public async Task<ApiResponse<GetPhotosResponse>> GetPhoto([FromQuery] GetPhotosRequest model)
    {
        model.UserId = this.UserId();
        return await _photosService.GetPhotos(model);
    } 
    
    
    [Authorize]
    [HttpPost("AddPhoto")]
    public async Task<ApiResponse<string>> AddPhoto([FromForm] AddPhotosRequest model)
    {
        model.UserId = this.UserId();
        return await _photosService.AddPhotos(model);
    } 
    
    [Authorize]
    [HttpDelete("DeletePhoto")]
    public async Task<ApiResponse<bool>> DeletePhoto([FromForm] DeletePhotoRequest model)
    {
        model.UserId = this.UserId();
        return await _photosService.DeletePhoto(model);
    } 
}