namespace SaaSPlatform.Web.Models.DTOs;

public class WeatherForecastDto
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF { get; set; }
}
