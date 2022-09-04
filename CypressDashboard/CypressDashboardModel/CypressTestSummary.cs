using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypressDashboardModel
{
    public class CypressTestSummary
    {
        public string _id { get; set; }
        public int totalDuration { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int nTests { get; set; }
        public int nTestsPassed { get; set; }
        public int nTestsFailed { get; set; }


        public String TotalDurationStr
        {
            get
            {
                if (totalDuration < 1000)
                    return totalDuration.ToString() + "ms";
                if (totalDuration >= 1000 & totalDuration < 60000)
                    return Math.Round((double)totalDuration / 1000, 0).ToString() + "s";
                if (totalDuration > 60000)
                    return Math.Floor((double)totalDuration / 60000).ToString() + "m " + Math.Round((double)(totalDuration % 60000) / 1000, 0) + "s";
                return "";

            }
        }


    }
}
