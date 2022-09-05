using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CypressDashboardModel;
using CypressDashboard;

namespace MongoWS.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CypressTestController : ControllerBase
	{
		
		private readonly ILogger<CypressTestController> _logger;

		public CypressTestController(ILogger<CypressTestController> logger)
		{
			_logger = logger;
		}

       
        [HttpGet("~/GetLatestSummary")]
        public IEnumerable<CypressTestSummary> GetLatestSummary(int nCountMax)
		{
			MongoDBAdapter adapter = new MongoDBAdapter();
			return adapter.GetLatestSummary(nCountMax);
		}

        [HttpGet("~/GetLatestTests")]
        public IEnumerable<CypressTest> GetLatestTests(int nCountMax)
        {
            MongoDBAdapter adapter = new MongoDBAdapter();
            return adapter.GetLatestTests(nCountMax);
        }


        [HttpGet("~/GetTest")]
        public IEnumerable<CypressTest> GetTest(string uid)
        {
            MongoDBAdapter adapter = new MongoDBAdapter();
            return adapter.GetTest(uid);
        }
        //[HttpGet(Name = "GetLastTests2")]
        //public IEnumerable<CypressDashboard.CypressTest> Get2(int nCountMax)
        //{
        //    CypressDashboard.MongoDBAdapter adapter = new CypressDashboard.MongoDBAdapter();
        //    return adapter.GetLastTests(nCountMax);
        //}
    }
}