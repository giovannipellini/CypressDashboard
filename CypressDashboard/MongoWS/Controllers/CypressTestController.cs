using Microsoft.AspNetCore.Mvc;

namespace MongoWS.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CypressTestController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<CypressTestController> _logger;

		public CypressTestController(ILogger<CypressTestController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<CypressDashboard.CypressTest> Get(int nCountMax)
		{
			CypressDashboard.MongoDBAdapter adapter = new CypressDashboard.MongoDBAdapter();
			return adapter.GetLastTests(nCountMax);
		}
	}
}