using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using CypressDashboardModel;
using System.Security.Cryptography;

namespace CypressDashboard
{
    public class MongoDBAdapter
    {
        IMongoCollection<CypressTest> testCollection;
        IMongoCollection<CypressTestSummary> testSummaryCollection;
        IMongoDatabase db;
        public MongoDBAdapter()
        {

            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            db = client.GetDatabase("Cypress");

            testCollection = db.GetCollection<CypressTest>("Tests");
            testSummaryCollection = db.GetCollection<CypressTestSummary>("TestSummary");
        }
        /// <summary>
        /// Returns the last nCountMax tests from the DB
        /// </summary>
        /// <param name="nCountMax">max number of returned tests</param>
        /// <returns></returns>
        public List<CypressTest> GetLatestTests(int nCountMax)
        {
            return testCollection.Find(FilterDefinition<CypressTest>.Empty).SortBy(field: x => x.stats.start).Limit(nCountMax).ToList();
        }

        public List<CypressTest> GetTest(string uid)
        {
            return testCollection.Find(x => x.stats.uid == uid).ToList();
        }

        public List<CypressTestSummary> GetLatestSummary(int nCountMax)
        {

            return testSummaryCollection.Find(FilterDefinition<CypressTestSummary>.Empty).Limit(nCountMax).ToList();

        }


        public List<TestDuration> GetTestDuration(string testName)
        {
            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<CypressTest> filterDefinition = "{\"results.suites.tests.title\":'" + testName + "'}";

            List<CypressTest> result = testCollection.Find(filterDefinition).Project<CypressTest>("{_id: 0,\"results.suites.tests.duration\":1,\"stats.start\":1}").ToList();
            //CypressTestDuration cypressTestDuration = new TestDuration();
            List<TestDuration> testDurations = new List<TestDuration>();
            //cypressTestDuration.testDurations = new List<TestDuration>();
            foreach (var item in result)
            {
                TestDuration testDuration = new TestDuration();
                testDuration.duration = (int)item.results[0].suites[0].tests[0].duration;
                testDuration.startDate = item.stats.start;
                testDurations.Add(testDuration);
                //cypressTestDuration.testDurations.Add(testDuration);
            }
            //List<TestDuration> newList = new List<TestDuration>();
            //newList.Add(cypressTestDuration);
            return testDurations;
        }


        public List<string> GetTestList()
        {
            List<string> testlist = new List<string>();

            testlist = testCollection.Distinct<string>("results.suites.tests.title", new BsonDocument()).ToList();

            return testlist;
        }
    }
}
