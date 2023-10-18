using GeneralDomain.Responses;
using UserDomain.ViewModels;

namespace UserApplication.Services;

public interface IUserServices
{
    Task<ApiResponse<bool>> RegisterUser(UserRegisterRequest request);
    Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
    Task<ApiResponse<bool>> AddFriend(AddFriendRequest request);
    Task<ApiResponse<bool>> DeleteFriend(DeleteFriendRequest request);
}