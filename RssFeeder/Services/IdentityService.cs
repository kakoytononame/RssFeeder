using RssFeeder.Contexts;
using RssFeeder.DTOS;
using RssFeeder.Models;

namespace RssFeeder.Services;

public class IdentityService:IIdentityService
{
    private readonly IJWTService _jwtService;

    private readonly DBContext _dbContext;

    public IdentityService(IJWTService jwtService,DBContext dbContext)
    {
        _jwtService = jwtService;
        _dbContext = dbContext;
    }
    public string Login(UserDTO user)
    {
        var jwt = "";
        var userEntity= _dbContext.IdentityUsers.FirstOrDefault(p => p.Login == user.Login);
        if (userEntity.Password == user.Password)
        {
            jwt=_jwtService.Create(new User
            {
                Login = user.Login,
                Password = user.Password,
                Role = userEntity.Role
            });
        }
        return jwt;
    }

    public string Register(UserDTO user)
    {
        _dbContext.IdentityUsers.Add(new IdentityUser
        {
            UserId = Guid.NewGuid(),
            Login = user.Login,
            Password = user.Password,
            Role = "user"
        });
        _dbContext.SaveChanges();
        var jwt= Login(user);
        return jwt;
    }
}