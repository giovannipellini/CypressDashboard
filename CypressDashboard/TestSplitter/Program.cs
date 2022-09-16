using CypressDashboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestSplitter
{
    class Program
    {
        static void Main(string[] args)
        {

            MongoDBAdapter adapter = new MongoDBAdapter();
            var lastUid = adapter.GetLatestTests(1).First().stats.uid;

            var lastTests = adapter.GetTest(lastUid);

            SortedDictionary<int, string> testStats = new();

            foreach (var file in Directory.GetFiles("C:\\MyFiles\\repo\\VPT\\cypress\\e2e"))
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
            List<string> servers = new() { "vs511", "vs512" };
            List<string> listOfArguments = Enumerable.Repeat<String>("", 2).ToList<String>();
            //C:\VitaPowerTool>node_modules\.bin\cypress run --spec "cypress\e2e\appunti.cy.js,cypress\e2e\login.cy.js"
            foreach (var item in testStats.OrderByDescending(x => x.Key))
            {
                if (listOfArguments[serverIndex].Length > 0) listOfArguments[serverIndex] += "@";
                listOfArguments[serverIndex] += item.Value.Replace(".cy.js", "");
                serverIndex++;
                if (serverIndex >= listOfArguments.Count) serverIndex = 0;
            }

            //serverIndex = 0;
            //foreach (var item in listOfArguments)
            //{
            //    client.GetStringAsync(cypressParallelData.serverList[serverIndex].Url + "?testList=" + item);
            //    serverIndex++;
            //}

            //await Task.WhenAll(listOfArguments.Select(item => client.GetStringAsync(cypressParallelData.serverList[listOfArguments.IndexOf(item)].Url + "?testList=" + item)));
           
            for (serverIndex = 0; serverIndex < servers.Count; serverIndex++)
            {
                string arg = "";
                foreach (var item in listOfArguments[serverIndex].Split('@'))
                {
                    if (arg.Length > 0) arg += ",";
                    arg += "cypress\\e2e\\" + item + ".cy.js";
                }
                string cmdFile = $".\\node_modules\\.bin\\cypress run --spec \"{arg}\"";
                

                switch (servers[serverIndex])
                {
                    case "vs511":
                        System.IO.File.WriteAllText("C:\\MyFiles\\repo\\VPT\\runCypress.bat", cmdFile);
                        break;
                    case "vs512":
                        System.IO.File.WriteAllText("\\\\vs512\\c$\\VitaPowerTool\\runCypress.bat", cmdFile);
                        break;
                    default:
                        break;
                }

            }

        }
    }
}
