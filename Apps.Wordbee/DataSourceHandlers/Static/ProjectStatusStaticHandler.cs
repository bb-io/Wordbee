using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Wordbee.DataSourceHandlers.Static;

public class ProjectStatusStaticHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            ["0"] = "In progress",
            ["1"] = "Completed",
            ["2"] = "Preparing",
            ["3"] = "Waiting",
            ["4"] = "Archived",
            ["10"] = "Cancelled",
            ["11"] = "Deletion pending",
        };
    }
}