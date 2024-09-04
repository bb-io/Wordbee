using Blackbird.Applications.Sdk.Common;

namespace Apps.Wordbee.Webhooks.Models.Payload;

public class WordbeeWebhookPayload
{
    [Display("User ID")]
    public string UserId { get; set; }

    public void SetUserId(string userId)
    {
        UserId = userId;
    }
}