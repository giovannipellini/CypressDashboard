using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace CypressDashboard
{
    public class MongoDBAdapter
    {
        IMongoCollection<CypressTest> testCollection;

        public MongoDBAdapter()
        {

            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            var db = client.GetDatabase("Cypress");

            testCollection = db.GetCollection<CypressTest>("Tests");
            
        }
        /// <summary>
        /// Returns the last nCountMax tests from the DB
        /// </summary>
        /// <param name="nCountMax">max number of returned tests</param>
        /// <returns></returns>
        public List<CypressTest> GetLastTests(int nCountMax)
        {
            var test = testCollection.Find(FilterDefinition<CypressTest>.Empty).SortBy(field: x => x.stats.start).Limit(nCountMax).ToList();
            return test;
        }


        //Console.WriteLine(test.Count);


    }
}
