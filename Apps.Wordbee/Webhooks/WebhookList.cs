using System.Net;
using Apps.Wordbee.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Wordbee.Webhooks;

[WebhookList]
public class WebhookList
{
    [Webhook("On project status changed", Description = "On status of any project changed")]
    public Task<WebhookResponse<ProjectStatusChangedPayload>> OnProjectStatusChanged(WebhookRequest request)
        => HandleCallback<ProjectStatusChangedPayload>(request);

    [Webhook("On order status changed", Description = "On status of any order changed")]
    public Task<WebhookResponse<OrderChangedPayload>> OnOrderStatusChanged(WebhookRequest request)
        => HandleCallback<OrderChangedPayload>(request);

    [Webhook("On order message", Description = "On any message is added to the order")]
    public Task<WebhookResponse<OrderChangedPayload>> OnOrderMessage(WebhookRequest request)
        => HandleCallback<OrderChangedPayload>(request);

    [Webhook("On order created", Description = "On a new order added")]
    public Task<WebhookResponse<OrderChangedPayload>> OnOrderCreated(WebhookRequest request)
        => HandleCallback<OrderChangedPayload>(request);

    [Webhook("On document workflow finished", Description = "On workflow of a specific document is finished")]
    public Task<WebhookResponse<DocumentWorkflowFinishedPayload>> OnDocumentWorkflowFinished(WebhookRequest request)
        => HandleCallback<DocumentWorkflowFinishedPayload>(request);


    [Webhook("On job status changed", Description = "On status of a specific job changed")]
    public Task<WebhookResponse<JobStatusChangedPayload>> OnJobStatusChanged(WebhookRequest request)
        => HandleCallback<JobStatusChangedPayload>(request);

    private Task<WebhookResponse<T>> HandleCallback<T>(WebhookRequest request) where T : WordbeeWebhookPayload
    {
        var requestBody = request.Body.ToString() ?? throw new ArgumentException("The callback body is empty");
        var payload = JsonConvert.DeserializeObject<WebhookPayload<T>>(requestBody) ??
                      throw new ArgumentException("The callback body is empty");

        payload.Data.SetUserId(payload.UserId);
        return Task.FromResult(new WebhookResponse<T>()
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = payload.Data
        });
    }
}