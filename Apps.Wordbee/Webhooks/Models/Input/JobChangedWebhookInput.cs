using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Webhooks.Models.Input;

public class JobChangedWebhookInput
{
    [Display("User ID")] public string? UserId { get; set; }

    [Display("Job ID")]
    [DataSource(typeof(JobDataSourceHandler))]
    public string? JobId { get; set; }
}