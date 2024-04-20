namespace WeatherAPI.DAL.Models;

public class RegionType
{
    public int Id { get; set; }
    public string Type { get; set; }

    public bool Validate()
    {
        return !string.IsNullOrWhiteSpace(Type);
    }
}