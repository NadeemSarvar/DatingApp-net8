using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

[Route("WeatherForecast")]
public class WeatherForecastController : BaseApiController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IConfiguration _config;
    private readonly JwtOption _jwtOtpion;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, IOptions<JwtOption> jwtOption)
    {
        _logger = logger;
        _config = config;
        _jwtOtpion = jwtOption.Value;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("getJwtConfig")]
    public IActionResult GetJWTOPtion()
    {
       var response = new {
            option = new { Issuer = _jwtOtpion.Issuer, Key = _jwtOtpion.Key, ExpDate = _jwtOtpion.ExpDate}
       };

        return Ok(response);
    }
}
