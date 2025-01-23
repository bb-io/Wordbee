using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Wordbee.DataSourceHandlers.Static
{
    public class JobStatusStaticHandler : IStaticDataSourceHandler
    {
        public Dictionary<string, string> GetData()
        {
            return new()
            {
                ["0"] = "Draft",
                ["1"] = "NotAssigned",
                ["2"] = "Inactive",
                ["3"] = "Proposal",
                ["4"] = "NotStarted",
                ["5"] = "InProgress",
                ["6"] = "Completed",
                ["7"] = "Delivered",
                ["8"] = "Archived",
                ["9"] = "Rejected",
                ["10"] = "Cancelled"
            };
        }
    }
}
