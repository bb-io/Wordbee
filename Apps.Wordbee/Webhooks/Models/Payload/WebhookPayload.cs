namespace Apps.Wordbee.Webhooks.Models.Payload;

public class WebhookPayload<T>
{
    public string UserId { get; set; }
    
    public T Data { get; set; }
}