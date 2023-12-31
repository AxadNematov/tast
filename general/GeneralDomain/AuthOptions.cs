using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GeneralDomain;

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    private const string KEY = "my_super_super_secret_secret_key!123";   // ключ для шифрации
    public const int LIFETIME = 9999; // время жизни токена - 1 минута
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
    public const string FilePath = "";

}