using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using CoffeeVietBE.DataAccess.UnitOfWork;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Model.Request;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Shared.Configurations;
using CoffeeVietBE.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeVietBE.Service
{
  public class UserService : IUserService
  {
    private readonly IMapper _mapper;
    private UserManager<User> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private readonly JwtSetting _setting;
    private readonly IUnitOfWork _unitofwork;

    public UserService(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSetting> setting, IUnitOfWork unitofwork)
    {
      _mapper = mapper;
      _userManager = userManager;
      _roleManager = roleManager;
      _setting = setting.Value;
      _unitofwork = unitofwork;
    }

    public async Task<BaseResponseModel> Register(RegisterModel model)
    {
      var user = await _userManager.FindByNameAsync(model.UserName);
      if (user != null)
      {
        throw new BadRequestException("Username has been taken.");
      }
      model.JoinedDate = DateTime.Now;
      var newCustomer = _mapper.Map<User>(model);
      newCustomer.UserType = "staff";

      var result = await _userManager.CreateAsync(newCustomer, model.Password);

      if (!result.Succeeded)
        throw new BadRequestException("User creation failed! Please check user details and try again.");
      if (!await _roleManager.RoleExistsAsync("Admin"))
        await _roleManager.CreateAsync(new IdentityRole("Admin"));

      if (await _roleManager.RoleExistsAsync("Admin"))
      {
        await _userManager.AddToRoleAsync(newCustomer, "Admin");
      }

      return new BaseResponseModel();
    }

    public async Task<AuthorizedResponseModel> Login(LoginModel userData)
    {

      var user = await _userManager.FindByNameAsync(userData.Username);
      if (user == null)
      {
        throw new BadRequestException("the user is not exist");
      }
      if (userData != null && await _userManager.CheckPasswordAsync(user, userData.Password))
      {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
          new Claim(ClaimTypes.Name, user.UserName),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var userRole in userRoles)
        {
          authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GetToken(authClaims);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = GenerateRefreshToken();
        var expriration = token.ValidTo;

        return new AuthorizedResponseModel(accessToken, refreshToken, expriration);
      }
      return new AuthorizedResponseModel("please contact your admin");
    }

    #region private method
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
      var authSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.Key));

      var signingCredentials = new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(
        issuer: _setting.Issuer,
        audience: _setting.Audience,
        expires: DateTime.Now.AddHours(3),
        claims: authClaims,
        signingCredentials: signingCredentials
      );

      return token;
    }

    private string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];
      using (var randomGenerator = RandomNumberGenerator.Create())
      {
        randomGenerator.GetBytes(randomNumber);
        string refreshToken = Convert.ToBase64String(randomNumber);
        return refreshToken;
      }
    }
    #endregion
  }
}