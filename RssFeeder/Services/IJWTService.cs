using Microsoft.AspNetCore.Http.HttpResults;
using RssFeeder.DTOS;
using RssFeeder.Models;

namespace RssFeeder.Services;

public interface IJWTService
{
    public string Create(User user);

    public CookieOptions GetCoockie(string Token);
}