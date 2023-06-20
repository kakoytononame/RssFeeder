using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RssFeeder.DTOS;
using RssFeeder.Models;
using RssFeeder.Services;

namespace RssFeeder.Controllers;
[Route("news/")]
public class NewsController:ControllerBase
{
    
    private readonly IIdentityService _identityService;

    private readonly IRssFeaderService _rssFeaderService;
    
    public NewsController(IIdentityService identityService,IRssFeaderService rssFeaderService)
    {
        _identityService = identityService;
        _rssFeaderService = rssFeaderService;
    }
    
    [HttpGet("add")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddNews(string URL)
    {
        
        return Ok(await _rssFeaderService.AddNews(URL)); 
    }
    [HttpPost("read")]
    public async Task ReadNews([FromBody]ReadNews news)
    {
        var token = Request.Headers.Authorization.ToString().Remove(0,7);
        var jwtToken = new JwtSecurityToken(token);
        var userId = jwtToken.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
        news.UserId = Guid.Parse(userId);
        _rssFeaderService.Read(news);
    }
    
    [Authorize]
    [HttpGet("readed")]
    public async Task<List<RssItem>> Readed()
    {
        var token = Request.Headers.Authorization.ToString().Remove(0,7);
        var jwtToken = new JwtSecurityToken(token);
        var userId = jwtToken.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
       return await _rssFeaderService.Readed(Guid.Parse(userId));
    }
    
    [HttpGet("unreaded")]
    public async Task<List<RssItem>> UnReaded()
    {
        var token = Request.Headers.Authorization.ToString().Remove(0,7);
        var jwtToken = new JwtSecurityToken(token);
        var userId = jwtToken.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
        return await _rssFeaderService.UnReaded(Guid.Parse(userId));
    }
    
    [HttpGet("get")]
    [Authorize]
    public async Task<List<RssItemWithFilters>> GetNews()
    {
        var token = Request.Headers.Authorization.ToString().Remove(0,7);
        var jwtToken = new JwtSecurityToken(token);
        var userId = jwtToken.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
       return await _rssFeaderService.GetNews(Guid.Parse(userId));
    }
}