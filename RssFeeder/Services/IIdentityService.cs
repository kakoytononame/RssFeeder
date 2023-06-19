using RssFeeder.DTOS;

namespace RssFeeder.Services;

public interface IIdentityService
{
    public string Login(UserDTO user);

    public string Register(UserDTO user);
}