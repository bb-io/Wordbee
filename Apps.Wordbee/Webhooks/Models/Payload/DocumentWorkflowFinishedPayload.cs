using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Webhooks.Models.Payload;

public class DocumentWorkflowFinishedPayload : WordbeeWebhookPayload
{
    [Display("Project ID")] public string? ProjectId { get; set; }

    [Display("Project reference")] public string ProjectReference { get; set; }

    [Display("Resource ID")] public string? ResourceId { get; set; }

    [Display("Client company ID")] public string? ClientCompanyId { get; set; }

    [Display("Document ID")] public string? DocumentId { get; set; }

    [Display("Document Nnme")] public string DocumentName { get; set; }

    [Display("Source locale")] public string SourceLocale { get; set; }

    [Display("Source locale name")] public string SourceLocaleName { get; set; }

    [Display("Target locale")] public string TargetLocale { get; set; }

    [Display("Target locale name")] public string TargetLocaleName { get; set; }

    [Display("Branch locale")] public string BranchLocale { get; set; }

    [Display("Branch locale name")] public string BranchLocaleName { get; set; }

    [Display("Status")] public string Status { get; set; }

    [Display("Status code")] public string StatusCode { get; set; }

    [Display("URL")] public string Url { get; set; }
}