/*using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace NAUCountryA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilterController : ControllerBase
    {
        

        private readonly ILogger<FilterController> _logger;

        public FilterController(ILogger<WeatherForecastController> logger)
        {
            _logger = (ILogger<FilterController>)logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                //Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}*/
