namespace WeatherAPI.BLL;

public interface IAuth
{
    bool Authenticate(IRequestCookieCollection cookieCollection);
}