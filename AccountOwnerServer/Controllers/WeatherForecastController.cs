using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILoggerManager _logger;
        private IRepositoryWrapper _repositoryWrapper;

        public WeatherForecastController(ILoggerManager logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    _logger.LogInfo("Here is info message from the controller.");
        //    _logger.LogDebug("Here is debug message from the controller.");
        //    _logger.LogWarning("Here is warn message from the controller.");
        //    _logger.LogError("Here is error message from the controller.");
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var domestincAccounts = _repositoryWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
            var owners = _repositoryWrapper.Owner.FindAll();

            return new string[] { "value1", "value2" };
        }
    }
}