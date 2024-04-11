using MinimalAPI.Model.Auth;

namespace MinimalAPI.Repository.Contract
{
    public interface ICustomTokenHandler
    {
        string GenerateToken(UserModel user);
    }
}
