using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Model;
using MinimalAPI.Model.Auth;
using MinimalAPI.Repository.Contract;
using MinimalAPI.Utilities;

namespace MinimalAPI.EndPoints
{
    public static class UserEndPoint
    {
        public static void UserEndPoints(this WebApplication app)
        {
            app.MapPost("/api/users/registration", RegisterUser)
                .Produces<APIResponse>(200)
                .Produces(400);
            app.MapPost("/api/users/login", LoginUser)
                .Produces<APIResponse>(200)
                .Produces(400);
            app.MapGet("/api/users", GetUser)
                .Produces<APIResponse>(200)
                .Produces(400);
        }

        public static APIResponse RegisterUser(IUserRepository userRepository, [FromBody] UserRegistration user)
        {
            if (userRepository.IsUserExist(user.UserName))
            {
                return APIStatusResponse.GetBadResponse("User name already exists..");
            }

            var isValidUser = userRepository.RegisterUser(user);

            if (isValidUser)
            {
                return APIStatusResponse.GetCreateResponse("User Created successfully");
            }

            return APIStatusResponse.GetBadResponse("failed to create user");

        }

        public static APIResponse LoginUser(IUserRepository userRepository, [FromBody] UserLogin userLogin)
        {
            if (!userRepository.IsUserExist(userLogin.UserName))
            {
                return APIStatusResponse.GetBadResponse("User name does not exists...");
            }

            var response = userRepository.Login(userLogin);

            if (response == null || response.Token == null)
            {
                return APIStatusResponse.GetBadResponse("User Name or password does not match..");
            }

            return APIStatusResponse.GetOkResponse(response);
        }

        public static APIResponse GetUser(IUserRepository userRepository)
        {
            var users = userRepository.GetUsers();

            return APIStatusResponse.GetOkResponse(users);
        }
    }
}