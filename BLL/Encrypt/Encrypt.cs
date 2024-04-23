using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WeatherAPI.BLL;

public class Encrypt : IEncrypt
{
    public string HashPassword(string password, string salt)
    {
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
            System.Text.Encoding.ASCII.GetBytes(salt),
            KeyDerivationPrf.HMACSHA512,
            5000,
            64));
        return hash;
    }
}