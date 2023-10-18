namespace UserDomain.ViewModels;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}