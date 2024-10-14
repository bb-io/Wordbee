using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Webhooks.Models.Input;

public class DocumentChangedWebhookInput
{
    [Display("User ID")] 
    [DataSource(typeof(UserDataHandler))]
    public string? UserId { get; set; }

    [Display("Resource ID")] 
    [DataSource(typeof(ResourceDataHandler))]
    public string? ResourceId { get; set; }

    [Display("Client company ID")] public string? ClientCompanyId { get; set; }
}