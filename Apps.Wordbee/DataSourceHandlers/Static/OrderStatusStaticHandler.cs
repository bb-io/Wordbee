using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Wordbee.DataSourceHandlers.Static
{
    public class OrderStatusStaticHandler : IStaticDataSourceHandler
    {
        public Dictionary<string, string> GetData()
        {
            return new()
            {
                ["0"] = "RequestSent",
                ["1"] = "Proposal",
                ["2"] = "Confirmed",
                ["3"] = "Done",
                ["4"] = "Approved",
                ["5"] = "Closed",
                ["10"] = "Cancelled"
            };
        }
    }
}
