using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RssFeeder.DTOS;
using RssFeeder.Services;

namespace RssFeeder.Controllers;
[Route("admin/")]
public class AdminController:ControllerBase
{
    private readonly IJWTService _jwtService;

    private readonly IIdentityService _identityService;

    private readonly IRssFeaderService _rssFeaderService;
    
    public AdminController(IJWTService jwtService,IIdentityService identityService,IRssFeaderService rssFeaderService)
    {
        _jwtService = jwtService;
        _identityService = identityService;
        _rssFeaderService = rssFeaderService;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public string Registration([FromBody]UserDTO ResitrationData)
    {
        var Token=_identityService.Register(ResitrationData);
        var cookieOptions = _jwtService.GetCoockie(Token);
        Response.Cookies.Append("AuthToken", Token, cookieOptions);
        return Token;
    }
    [HttpPost("login")]
    public string Login([FromBody]UserDTO LoginData)
    {

        var Token=_identityService.Login(LoginData);
        var cookieOptions = _jwtService.GetCoockie(Token);
        Response.Cookies.Append("AuthToken", Token, cookieOptions);
        return Token;
    }

}