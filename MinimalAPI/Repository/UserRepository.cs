using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Model.Auth;
using MinimalAPI.Repository.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<UserModel> _userModels;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ICustomTokenHandler _customTokenHandler;

        public UserRepository(List<UserModel> user, IMapper mapper, IConfiguration configuration, ICustomTokenHandler customTokenHandler)
        {
            _userModels = user;
            _mapper = mapper;
            _configuration = configuration;
            _customTokenHandler = customTokenHandler;
        }

        public bool RegisterUser(UserRegistration userDetail)
        {
            try
            {
                var user = _mapper.Map<UserModel>(userDetail);
                user.Created = DateTime.Now;
                user.Id = _userModels.Count + 1;

                _userModels.Add(user);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public UserLoginResponse Login(UserLogin userLogin)
        {
            var user = _userModels.FirstOrDefault(x => x.UserName == userLogin.UserName && x.Password == userLogin.Password);

            if (user == null)
            {
                return null;
            }

            var response = new UserLoginResponse
            {
                User = _mapper.Map<UserResponse>(user),
                Token = _customTokenHandler.GenerateToken(user)
            };

            return response;
        }

        public bool IsUserExist(string userName)
        {
            return _userModels.Any(x => x.UserName == userName);
        }

        public List<UserResponse> GetUsers()
        {
            return _mapper.Map<List<UserResponse>>(_userModels);
        }
    }
}