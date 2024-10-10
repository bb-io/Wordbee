using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Wordbee.Webhooks.Models.Payload;

public class OrderChangedPayload : WordbeeWebhookPayload
{
    [Display("Order ID")]
    public string Id { get; set; }

    public int Status { get; set; }

    [Display("Status code")]
    public string StatusCode { get; set; }

    [Display("Status name")]
    public string StatusName { get; set; }

    [Display("Initial status")]
    public int? InitialStatus { get; set; }

    [Display("Initial status code")]
    public string InitialStatusCode { get; set; }

    [Display("Initial status name")]
    public string InitialStatusName { get; set; }

    [Display("Reference")]
    public string Reference { get; set; }

    [Display("Client user ID")]
    public string? ClientUserId { get; set; } 

    [Display("Project ID")]
    public string ProjectId { get; set; }

    [Display("Deadline"), JsonProperty("deadline_x")]
    public DateTime? DeadlineX { get; set; }

    [Display("Message")]
    public string Message { get; set; }

    [Display("Order URL")]
    public string Url { get; set; }
}