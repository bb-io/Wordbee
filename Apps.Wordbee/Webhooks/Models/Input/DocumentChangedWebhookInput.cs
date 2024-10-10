using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Webhooks.Models.Input;

public class DocumentChangedWebhookInput
{
    [Display("User ID")] public string? UserId { get; set; }

    [Display("Project ID")]
    [DataSource(typeof(ProjectDataSourceHandler))]
    public string? ProjectId { get; set; }

    [Display("Resource ID")] public string? ResourceId { get; set; }

    [Display("Client company ID")] public string? ClientCompanyId { get; set; }

    [Display("Document ID")] public string? DocumentId { get; set; }
}