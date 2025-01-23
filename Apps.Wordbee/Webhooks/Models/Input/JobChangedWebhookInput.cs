using Apps.Wordbee.DataSourceHandlers;
using Apps.Wordbee.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Webhooks.Models.Input;

public class JobChangedWebhookInput
{
    [Display("User ID")]
    [DataSource(typeof(UserDataHandler))]
    public string? UserId { get; set; }

    [Display("Job ID")]
    [DataSource(typeof(JobDataSourceHandler))]
    public string? JobId { get; set; }

    [Display("Job status")]
    [StaticDataSource(typeof(JobStatusStaticHandler))]
    public string? Status { get; set; }
}