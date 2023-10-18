using System.Net;
using GeneralApplication.Extensions;
using GeneralDomain.EntityModels;
using GeneralDomain.Extensions;
using GeneralDomain.Responses;
using GeneralInfrastructure.Db;
using UserDomain.ViewModels;

namespace UserApplication.Services;

public class UserService:IUserServices
{
    private readonly DataContext _dbContext;

    public UserService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    

    public async Task<ApiResponse<bool>> RegisterUser(UserRegisterRequest request)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.UserName == request.UserName);

        if (user != null)
            return new ApiResponseError(HttpStatusCode.NotAcceptable, "Login is occupied!");
        
        User addUser = new User()
        {
            UserName = request.UserName,
            Password = request.Password.GetHashString(),
            Email = request.Email
        };

        _dbContext.Add(addUser);
        _dbContext.SaveChanges();

        return true;
    }

    public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
    {
        if (!String.IsNullOrEmpty(request.Password) && !String.IsNullOrEmpty(request.UserName))
        {
            var user = _dbContext.User.FirstOrDefault(u => u.UserName.ToLower() == request.UserName.ToLower() && u.Password == request.Password.GetHashString());
            if(user==null)
                return new ApiResponseError(HttpStatusCode.Unauthorized, "UserNotFound");
        
            return new LoginResponse()
            {
                IsSuccess = true,
                AccessToken = UserIdentity.AccessToken(user),
                RefreshToken = UserIdentity.RefreshToken(user)
            };
        }
        else
        {
            return new ApiResponseError(HttpStatusCode.BadRequest, "not enough data");
        }   
    }

    public async Task<ApiResponse<bool>> AddFriend(AddFriendRequest request)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == request.UserId);
        if (user == null)
            return new ApiResponseError(HttpStatusCode.NotFound, "user not found");
        
        var friend = _dbContext.User.FirstOrDefault(u => u.UserName == request.FriendUserName);
        if (friend == null)
            return new ApiResponseError(HttpStatusCode.NotFound, "friend not found");
        
        var userFriend = _dbContext.UserFriends.FirstOrDefault(u => u.UserId == request.UserId && u.FriendId == friend.Id);
        if (userFriend != null)
            return new ApiResponseError(HttpStatusCode.MethodNotAllowed, "friend already exist");

        var addFriend = new UserFriends()
        {
            UserId = request.UserId,
            FriendId = friend.Id
        };

        _dbContext.UserFriends.Add(addFriend);
        _dbContext.SaveChanges();
        return true;
    }
    public async Task<ApiResponse<bool>> DeleteFriend(DeleteFriendRequest request)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == request.UserId);
        if (user == null)
            return new ApiResponseError(HttpStatusCode.NotFound, "user not found");
        
        var friend = _dbContext.User.FirstOrDefault(u => u.UserName == request.FriendUserName);
        if (friend == null)
            return new ApiResponseError(HttpStatusCode.NotFound, "friend user found");
        
        var userFriend = _dbContext.UserFriends.FirstOrDefault(u => u.UserId == request.UserId && u.FriendId == friend.Id);
        if (userFriend == null)
            return new ApiResponseError(HttpStatusCode.MethodNotAllowed, "friend not exist");
        

        _dbContext.UserFriends.Remove(userFriend);
        _dbContext.SaveChanges();
        return true;
    }
}