using System.Net;
using Apps.Wordbee.Webhooks.Models.Input;
using Apps.Wordbee.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Wordbee.Webhooks;

[WebhookList]
public class WebhookList
{
    [Webhook("On project status changed", Description = "On status of any project changed")]
    public Task<WebhookResponse<ProjectStatusChangedPayload>> OnProjectStatusChanged(WebhookRequest request,
        [WebhookParameter] ProjectWebhookInput input)
    {
        var data = HandleCallback<ProjectStatusChangedPayload>(request);

        if (input.ProjectId is not null && data.Id != input.ProjectId)
            return Task.FromResult(GetPreflightResponse<ProjectStatusChangedPayload>());

        if (input.UserId is not null && data.UserId != input.UserId)
            return Task.FromResult(GetPreflightResponse<ProjectStatusChangedPayload>());

        return Task.FromResult(new WebhookResponse<ProjectStatusChangedPayload>()
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = data
        });
    }

    [Webhook("On order status changed", Description = "On status of any order changed")]
    public Task<WebhookResponse<OrderChangedPayload>> OnOrderStatusChanged(WebhookRequest request,
        [WebhookParameter] OrderChangedWebhookInput input)
    {
        var data = HandleCallback<OrderChangedPayload>(request);

        if (input.ProjectId is not null && data.ProjectId != input.ProjectId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.UserId is not null && data.UserId != input.UserId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.ClientUserId is not null && data.ClientUserId != input.ClientUserId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.OrderId is not null && data.Id != input.OrderId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        return Task.FromResult(new WebhookResponse<OrderChangedPayload>()
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = data
        });
    }

    [Webhook("On order message", Description = "On any message is added to the order")]
    public Task<WebhookResponse<OrderChangedPayload>> OnOrderMessage(WebhookRequest request,
        [WebhookParameter] OrderChangedWebhookInput input)
    {
        var data = HandleCallback<OrderChangedPayload>(request);

        if (input.ProjectId is not null && data.ProjectId != input.ProjectId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.UserId is not null && data.UserId != input.UserId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.ClientUserId is not null && data.ClientUserId != input.ClientUserId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.OrderId is not null && data.Id != input.OrderId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        return Task.FromResult(new WebhookResponse<OrderChangedPayload>()
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = data
        });
    }

    [Webhook("On order created", Description = "On a new order added")]
    public Task<WebhookResponse<OrderChangedPayload>> OnOrderCreated(WebhookRequest request,
        [WebhookParameter] OrderCreatedWebhookInput input)
    {
        var data = HandleCallback<OrderChangedPayload>(request);

        if (input.ProjectId is not null && data.ProjectId != input.ProjectId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.UserId is not null && data.UserId != input.UserId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        if (input.ClientUserId is not null && data.ClientUserId != input.ClientUserId)
            return Task.FromResult(GetPreflightResponse<OrderChangedPayload>());

        return Task.FromResult(new WebhookResponse<OrderChangedPayload>()
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = data
        });
    }

    [Webhook("On document workflow finished", Description = "On workflow of a specific document is finished")]
    public Task<WebhookResponse<DocumentWorkflowFinishedPayload>> OnDocumentWorkflowFinished(WebhookRequest request,
        [WebhookParameter] DocumentChangedWebhookInput input)
    {
        var data = HandleCallback<DocumentWorkflowFinishedPayload>(request);

        if (input.ProjectId is not null && data.ProjectId != input.ProjectId)
            return Task.FromResult(GetPreflightResponse<DocumentWorkflowFinishedPayload>());

        if (input.UserId is not null && data.UserId != input.UserId)
            return Task.FromResult(GetPreflightResponse<DocumentWorkflowFinishedPayload>());

        if (input.ResourceId is not null && data.ResourceId != input.ResourceId)
            return Task.FromResult(GetPreflightResponse<DocumentWorkflowFinishedPayload>());

        if (input.ClientCompanyId is not null && data.ClientCompanyId != input.ClientCompanyId)
            return Task.FromResult(GetPreflightResponse<DocumentWorkflowFinishedPayload>());

        if (input.DocumentId is not null && data.DocumentId != input.DocumentId)
            return Task.FromResult(GetPreflightResponse<DocumentWorkflowFinishedPayload>());

        return Task.FromResult(new WebhookResponse<DocumentWorkflowFinishedPayload>()
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = data
        });
    }

    [Webhook("On job status changed", Description = "On status of a specific job changed")]
    public Task<WebhookResponse<JobStatusChangedPayload>> OnJobStatusChanged(WebhookRequest request,
        [WebhookParameter] JobChangedWebhookInput input)
    {
        var data = HandleCallback<JobStatusChangedPayload>(request);

        if (input.JobId is not null && data.JobId != input.JobId)
            return Task.FromResult(GetPreflightResponse<JobStatusChangedPayload>());

        if (input.UserId is not null && data.UserId != input.UserId)
            return Task.FromResult(GetPreflightResponse<JobStatusChangedPayload>());

        return Task.FromResult(new WebhookResponse<JobStatusChangedPayload>()
        {
            HttpResponseMessage = new(HttpStatusCode.OK),
            Result = data
        });
    }

    private T HandleCallback<T>(WebhookRequest request) where T : WordbeeWebhookPayload
    {
        var requestBody = request.Body.ToString() ?? throw new ArgumentException("The callback body is empty");
        var payload = JsonConvert.DeserializeObject<WebhookPayload<T>>(requestBody) ??
                      throw new ArgumentException("The callback body is empty");

        payload.Data.SetUserId(payload.UserId);
        return payload.Data;
    }

    private WebhookResponse<T> GetPreflightResponse<T>() where T : class => new()
    {
        HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
        ReceivedWebhookRequestType = WebhookRequestType.Preflight
    };
}