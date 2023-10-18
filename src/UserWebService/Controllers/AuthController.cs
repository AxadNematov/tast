using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Services;
using UserDomain.ViewModels;

namespace UserWebService.Controllers;
[Route("auth/[controller]")]
[ApiController]
public class AuthController:ControllerBase
{
    private readonly IUserServices _userServices;

    public AuthController(IUserServices userService)
    {
        _userServices = userService;
    }
    
    
    [HttpPost("Register")]
    public async Task<ApiResponse<bool>> Register([FromBody] UserRegisterRequest model)
        => await _userServices.RegisterUser(model);
    
    
    
    [HttpPost("Login")]
    public async Task<ApiResponse<LoginResponse>> Login([FromBody] LoginRequest model)
        => await _userServices.Login(model);
    
    
    
    [Authorize]
    [HttpPost("AddFriend")]
    public async Task<ApiResponse<bool>> AddFriend([FromBody] AddFriendRequest model)
    {
        model.UserId = this.UserId();
        return await _userServices.AddFriend(model);
    } 
    
    
    [Authorize]
    [HttpDelete("DeleteFriend")]
    public async Task<ApiResponse<bool>> DeleteFriend([FromBody] DeleteFriendRequest model)
    {
        model.UserId = this.UserId();
        return await _userServices.DeleteFriend(model);
    } 
}