using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Wordbee.Webhooks.Models.Payload;

public class ProjectStatusChangedPayload : WordbeeWebhookPayload
{
    [Display("Project ID")]
    public string Id { get; set; }

    public int Status { get; set; }

    [Display("Status code")]
    public string StatusCode { get; set; }

    [Display("Status name")]
    public string StatusName { get; set; }

    [Display("Initial status")]
    public int InitialStatus { get; set; }

    [Display("Initial status code")]
    public string InitialStatusCode { get; set; }

    [Display("Initial status name")]
    public string InitialStatusName { get; set; }

    [Display("Reference")]
    public string Reference { get; set; }

    [Display("Deadline"), JsonProperty("deadline_x")]
    public DateTime DeadlineX { get; set; }

    [Display("Project URL")]
    public string Url { get; set; }
}