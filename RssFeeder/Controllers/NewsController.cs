using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        _rssFeaderService.Read(news);
    }

    [HttpGet("get")]
    
    public async Task<List<RssItem>> GetNews(int number)
    {
       return await _rssFeaderService.GetNews(number);
    }
}