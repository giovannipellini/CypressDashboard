using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using CypressDashboardModel;

namespace CypressDashboard
{
    public class MongoDBAdapter
    {
        IMongoCollection<CypressTest> testCollection;
        IMongoCollection<CypressTestSummary> testSummaryCollection;

        public MongoDBAdapter()
        {

            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            var db = client.GetDatabase("Cypress");

            testCollection = db.GetCollection<CypressTest>("Tests");
            testSummaryCollection = db.GetCollection<CypressTestSummary>("TestSummary");
        }
        /// <summary>
        /// Returns the last nCountMax tests from the DB
        /// </summary>
        /// <param name="nCountMax">max number of returned tests</param>
        /// <returns></returns>
        public List<CypressTest> GetLastTests(int nCountMax)
        {
            return testCollection.Find(FilterDefinition<CypressTest>.Empty).SortBy(field: x => x.stats.start).Limit(nCountMax).ToList();
        }

        public List<CypressTestSummary> GetLatestRuns(int nCountMax)
        {

            return testSummaryCollection.Find(FilterDefinition<CypressTestSummary>.Empty).Limit(nCountMax).ToList();

        }


    }
}
