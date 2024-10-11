using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Webhooks.Models.Input;

public class OrderCreatedWebhookInput
{
    [Display("User ID")]
    [DataSource(typeof(UserDataHandler))]
    public string? UserId { get; set; }
    
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataSourceHandler))]
    public string? ProjectId { get; set; }
    
    [Display("Client user ID")]
    public string? ClientUserId { get; set; } 
}