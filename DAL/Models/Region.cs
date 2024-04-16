namespace WeatherAPI.DAL.Models;

public class Region
{
    public int Id { get; set; }
    public int RegionType { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
    public string Name { get; set; }
    public string? ParentRegion { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool Validate() => Name != null && Latitude != null && Longitude != null;
}