namespace WeatherAPI.DAL.Models;

public class Account
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }

    private bool ValidateName()
    {
        if (FirstName == null || LastName == null)
            return false;
        
        var checkFN = FirstName.Replace(" ", "");
        var checkLN = LastName.Replace(" ", "");

        if (checkFN.Length == 0 || checkLN.Length == 0)
            return false;

        return true;
    }
    
    private bool ValidateEmailPassword()
    {
        if (Email == null || Password == null)
            return false;
        
        var checkEmail = Email.Replace(" ", "");
        var checkPassword = Password.Replace(" ", "");

        if (checkEmail.Length == 0 || checkPassword.Length == 0)
            return false;

        return true;
    }

    public bool Validate()
    {
        return ValidateName() && ValidateEmailPassword();
    }
}