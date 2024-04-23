using WeatherAPI.Controllers;

namespace WeatherAPI.BLL;

public class Auth : IAuth
{
    public bool Authenticate(IRequestCookieCollection cookieCollection)
    {
        var cookieValue = cookieCollection["UserId"];
        if (cookieValue == null)
            return false;
        return true;
    }
}