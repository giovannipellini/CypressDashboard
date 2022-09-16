using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CypressDashboardModel;
using CypressDashboard;
using System.IO;
using System.Collections.Specialized;

namespace CypressWS.Controllers
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


        public class CypressServerData
		{
			public string Url { get; set; } = "";
            public string Path { get; set; } = "";
        }
        public class CypressParallelData
        {
            public string Path { get; set; } = ".\\cypress\\e2e";

			public List<CypressServerData> serverList { get; set; } = new ();

        }
        [HttpPost("~/StartTest")]
        public void StartAllTests(CypressParallelData cypressParallelData)
		{
			MongoDBAdapter adapter = new MongoDBAdapter();
			var lastUid = adapter.GetLatestTests(1).First().stats.uid;

			var lastTests = adapter.GetTest(lastUid);

			SortedDictionary<int, string> testStats = new ();

			foreach (var file in System.IO.Directory.GetFiles(cypressParallelData.Path))
			{
                foreach (var test in lastTests)
                {
                    if (test.results.First().file==file)
                    {
                        int a = 5;
                    }
                }
            }

        }

        [HttpGet("~/GetTestList")]
        public List<string> GetTestList()
        {
            MongoDBAdapter adapter = new MongoDBAdapter();
            return adapter.GetTestList();
        }
    }
}