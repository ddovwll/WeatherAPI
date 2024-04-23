using Microsoft.VisualBasic;

namespace WeatherAPI.DAL.Models;

public class Weather
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public float WindSpeed { get; set; }
    public string WeatherCondition { get; set; }
    public float PrecipitationAmount { get; set; }
    public DateTime DateTime { get; set; }
    public List<WeatherForecast> WeatherForecast { get; set; }
}