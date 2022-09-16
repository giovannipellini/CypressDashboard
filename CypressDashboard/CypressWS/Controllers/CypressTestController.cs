using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CypressDashboardModel;
using CypressDashboard;
using System.IO;
using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using System.Diagnostics;

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

            public List<CypressServerData> serverList { get; set; } = new();

        }
        [HttpPost("~/StartTest")]
        public async void StartAllTests(CypressParallelData cypressParallelData)
        {
            MongoDBAdapter adapter = new MongoDBAdapter();
            var lastUid = adapter.GetLatestTests(1).First().stats.uid;

            var lastTests = adapter.GetTest(lastUid);

            SortedDictionary<int, string> testStats = new();

            foreach (var file in System.IO.Directory.GetFiles(cypressParallelData.Path))
            {
                var fi = new FileInfo(file);
                bool found = false;

                foreach (var test in lastTests)
                {
                    string testName = test.results.First().file.Split('\\').Last();
                    if (fi.Name == testName)
                    {
                        testStats.Add((int)test.stats.duration, testName);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    testStats.Add(int.MaxValue, fi.Name);
                }
            }

            int serverIndex = 0;
            List<string> listOfArguments = Enumerable.Repeat<String>("", cypressParallelData.serverList.Count).ToList<String>();
            //C:\VitaPowerTool>node_modules\.bin\cypress run --spec "cypress\e2e\appunti.cy.js,cypress\e2e\login.cy.js"
            foreach (var item in testStats.OrderByDescending(x => x.Key))
            {
                if (listOfArguments[serverIndex].Length > 0) listOfArguments[serverIndex] += "@";
                listOfArguments[serverIndex] += item.Value.Replace(".cy.js", "");
                serverIndex++;
                if (serverIndex >= cypressParallelData.serverList.Count) serverIndex = 0;
            }

            //serverIndex = 0;
            //foreach (var item in listOfArguments)
            //{
            //    client.GetStringAsync(cypressParallelData.serverList[serverIndex].Url + "?testList=" + item);
            //    serverIndex++;
            //}

            await Task.WhenAll(listOfArguments.Select(item => client.GetStringAsync(cypressParallelData.serverList[listOfArguments.IndexOf(item)].Url + "?testList=" + item)));


        }

        [HttpGet("~/RunTest")]
        public void RunTest(string testList)
        {
            string arg = "";
            foreach (var item in testList.Split('@'))
            {
                if (arg.Length > 0) arg += ",";
                arg += "cypress\\e2e\\" + item + ".cy.js";
            }

            string cmdFile = $".\\node_modules\\.bin\\cypress run --spec \"{arg}\"";
            System.IO.File.WriteAllText("C:\\VitaPowerTool\\runCypress.bat", cmdFile);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("C:\\VitaPowerTool\\runCypress.bat");

            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.WorkingDirectory = "C:\\VitaPowerTool"; //\\node_modules\\.bin";
            //startInfo.FileName = "runCypress.bat";
            //startInfo.FileName = "cypress.cmd";
            //startInfo.Arguments = $"run cypress --spec \"{arg}\" ";
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();
            string s = process.StandardOutput.ReadToEnd();

            //MongoDBAdapter adapter = new MongoDBAdapter();
            //return adapter.GetTestList();
        }

        static HttpClient client = new HttpClient();

        private static async Task ProcessRepositories()
        {
            var responseString = await client.GetStringAsync("http://www.example.com/recepticle.aspx");

        }
    }
}