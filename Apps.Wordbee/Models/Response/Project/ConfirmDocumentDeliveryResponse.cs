using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Wordbee.Models.Response.Project;

public class ConfirmDocumentDeliveryResponse
{
    [JsonProperty("changed")] public bool Success { get; set; }

    [Display("Status code"), JsonProperty("status")]
    public int StatusCode { get; set; }

    [JsonProperty("statusTitle")] public string Status { get; set; }
}