using AutoMapper;
using MinimalAPI.Model;
using MinimalAPI.Model.Auth;

namespace MinimalAPI.Automapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AddCouponModel, Coupon>();
            CreateMap<UserRegistration, UserModel>();
            CreateMap<UserModel, UserResponse>();
        }
    }
}
