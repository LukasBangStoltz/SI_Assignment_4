using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IKafkaProducerService _kafkaProducerService;

        public WeatherForecastController(IKafkaProducerService kafkaProducerService)
        {
            _kafkaProducerService= kafkaProducerService;
        }

        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<bool> Get()
        {
            return await _kafkaProducerService.SendReviewRequest("create_review", "Hello");
        }
    }
}