namespace WeatherAPI.DAL.Models;

public class WeatherForecast
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int WeatherId { get; set; }
    public Weather Weather { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }
}