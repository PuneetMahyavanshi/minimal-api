using MinimalAPI.Model.Auth;

namespace MinimalAPI.Repository.Contract
{
    public interface IUserRepository
    {
        bool RegisterUser(UserRegistration userDetail);

        UserLoginResponse Login(UserLogin userLogin);

        bool IsUserExist(string userName);

        List<UserResponse> GetUsers();
    }
}
