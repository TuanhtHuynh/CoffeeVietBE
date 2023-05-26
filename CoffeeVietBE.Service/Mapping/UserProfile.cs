using AutoMapper;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Model;
using CoffeeVietBE.Model.Request;

namespace CoffeeVietBE.Service.Mapping
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, AppUserModel>();
      CreateMap<RegisterModel, User>();
    }
  }
}