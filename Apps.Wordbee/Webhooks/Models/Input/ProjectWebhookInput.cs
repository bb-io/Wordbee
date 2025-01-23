using Apps.Wordbee.DataSourceHandlers;
using Apps.Wordbee.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Webhooks.Models.Input;

public class ProjectWebhookInput
{
    [Display("User ID")]
    [DataSource(typeof(UserDataHandler))]
    public string? UserId { get; set; }

    [Display("Project ID")]
    [DataSource(typeof(ProjectDataSourceHandler))]
    public string? ProjectId { get; set; }

    [Display("Project status")]
    [StaticDataSource(typeof(ProjectStatusStaticHandler))]
    public string? Status { get; set; }
}