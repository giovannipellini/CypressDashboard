using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("~/getsomething")]
        public IEnumerable<CypressDashboard.CypressTest> Get(int nCountMax)
		{
			CypressDashboard.MongoDBAdapter adapter = new CypressDashboard.MongoDBAdapter();
			return adapter.GetLastTests(nCountMax);
		}

        [HttpGet("~/getothersomething")]
        public IEnumerable<CypressDashboard.CypressTest> GetOtherTests()
        {
            CypressDashboard.MongoDBAdapter adapter = new CypressDashboard.MongoDBAdapter();
            return adapter.GetLastTests(1);
        }

        //[HttpGet(Name = "GetLastTests2")]
        //public IEnumerable<CypressDashboard.CypressTest> Get2(int nCountMax)
        //{
        //    CypressDashboard.MongoDBAdapter adapter = new CypressDashboard.MongoDBAdapter();
        //    return adapter.GetLastTests(nCountMax);
        //}
    }
}