using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RssFeeder.DTOS;
using RssFeeder.Models;

namespace RssFeeder.Services;
#pragma warning disable CS8600
#pragma warning disable CS8603
public class JWTService:IJWTService
{
    public string Create(User user)
    {
        try
        {
            var securityKey = AuthOptions.GetSymmetricSecurityKey();
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = GetIdentity(user);

            var token = new JwtSecurityToken(AuthOptions.ISSUER, AuthOptions.AUDIENCE, claims.Claims,
                expires: DateTime.Now.AddMinutes(60), signingCredentials: credintials);

            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
        catch
        {
            return "";
        }

        
    }
    private ClaimsIdentity GetIdentity(User user)
    {

        var _people = new List<User>() { user };
        var person = _people.FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password);
        // ReSharper disable once InvertIf
        if (person != null)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, person.Login),
                new (ClaimsIdentity.DefaultRoleClaimType,person.Role)
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        return null;

    }

    public CookieOptions GetCoockie(string Token)
    {
        // Создание объекта cookie
        var cookieOptions = new CookieOptions
        {
            // Установка времени жизни cookie (например, на 1 час)
            Expires = DateTime.Now.AddHours(1),
            // Установка флага HttpOnly для защиты от скриптового доступа
            HttpOnly = true,
            // Установка пути, где будет доступна cookie
            Path = "/"
        };
        return cookieOptions;
    }
    
}