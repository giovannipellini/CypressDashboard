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

       
        [HttpGet("~/getsomething")]
        public IEnumerable<CypressTest> Get(int nCountMax)
		{
			MongoDBAdapter adapter = new MongoDBAdapter();
			return adapter.GetLastTests(nCountMax);
		}

        [HttpGet("~/getothersomething")]
        public IEnumerable<CypressTest> GetOtherTests()
        {
            MongoDBAdapter adapter = new MongoDBAdapter();
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