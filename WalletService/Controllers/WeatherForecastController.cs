using System.Linq;

namespace WalletService.Controllers;

[Microsoft.AspNetCore.Mvc.ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class WeatherForecastController : Microsoft.AspNetCore.Mvc.ControllerBase
{
	private static readonly string[] Summaries = new[]
	{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

	private readonly Microsoft.Extensions.Logging.ILogger<WeatherForecastController> _logger;

	public WeatherForecastController(Microsoft.Extensions.Logging.ILogger<WeatherForecastController> logger)
	{
		_logger = logger;
	}

	[Microsoft.AspNetCore.Mvc.HttpGet(Name = "GetWeatherForecast")]
	public System.Collections.Generic.IEnumerable<WeatherForecast> Get()
	{
		try
		{
			var databaseContext =
				new Data.DatabaseContext();
		}
		catch (System.Exception ex)
		{
			var message = ex.Message;
		}

		return System.Linq.Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = System.DateTime.Now.AddDays(index),
			TemperatureC = System.Random.Shared.Next(-20, 55),
			Summary = Summaries[System.Random.Shared.Next(Summaries.Length)]
		})
		.ToArray();
	}
}
