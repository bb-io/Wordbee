using Apps.Wordbee.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Wordbee.Models.Request.Project;

public class SearchProjectsRequest
{
    [StaticDataSource(typeof(ProjectStatusStaticHandler))]
    public string? Status { get; set; }
}