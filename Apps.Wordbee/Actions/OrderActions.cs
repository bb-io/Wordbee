using System.Net.Mime;
using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models;
using Apps.Wordbee.Models.Entities;
using Apps.Wordbee.Models.Request.Order;
using Apps.Wordbee.Models.Response;
using Apps.Wordbee.Models.Response.Order;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Wordbee.Actions;

[ActionList]
public class OrderActions : WordbeeInvocable
{
    private readonly IFileManagementClient _fileManagementClient;

    public OrderActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(
        invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("Download order files", Description = "Download all order files as ZIP")]
    public async Task<FileEntity> DownloadOrderFiles([ActionParameter] OrderRequest order)
    {
        var request = new WordbeeRequest($"orders/{order.OrderId}/files/all/zip", Method.Post, Creds);
        var response = await Client.ExecuteWithErrorHandling<AsyncOperationResponse>(request);

        var requestId = response.Trm.RequestId;
        do
        {
            await Task.Delay(2000);

            request = new WordbeeRequest($"trm/status?requestid={requestId}", Method.Get, Creds);
            response = await Client.ExecuteWithErrorHandling<AsyncOperationResponse>(request);

            if (response.Trm.Status == "Failed")
                throw new(response.Trm.StatusInfo);
        } while (response.Custom?.Filetoken is null);

        request = new WordbeeRequest($"media/get/{response.Custom.Filetoken}", Method.Get, Creds);
        var fileResponse = await Client.ExecuteWithErrorHandling(request);

        return new()
        {
            File = await _fileManagementClient.UploadAsync(new MemoryStream(fileResponse.RawBytes!),
                MediaTypeNames.Application.Zip, $"{order.OrderId}.zip")
        };
    }

    [Action("Search orders", Description = "Search for multiple orders")]
    public async Task<ListOrdersResponse> SearchOrders(
        [ActionParameter] SearchOrdersRequest input)
    {
        var request = new WordbeeRequest("orders/list", Method.Post, Creds).WithJsonBody(new
        {
            query = $"{{reference}}.Contains(\"{input.Reference}\")"
        });

        return new()
        {
            Orders = await Client.Paginate<OrderEntity>(request)
        };
    }

    [Action("Create order", Description = "Create a new order")]
    public async Task<OrderEntity> CreateOrder(
        [ActionParameter] CreateOrderRequest input,
        [ActionParameter] FileModel file)
    {
        var fileStream = await _fileManagementClient.DownloadAsync(file.File);
        var request = new WordbeeRequest("orders/create", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        };

        request.AddFile("zipFile", () => fileStream, file.File.Name);
        request.AddParameter("data", JsonConvert.SerializeObject(new
        {
            client = new
            {
                companyId = input.CompanyId
            },
            deadline = input.Deadline,
            reference = input.Reference,
            option = 0,
            sourceLanguage = input.SourceLanguage,
            targetLanguages = input.TargetLanguages,
        }));

        var response = await Client.ExecuteWithErrorHandling(request);

        var requestId = response.Content;
        AsyncOperationResponse trmResponse;
        do
        {
            await Task.Delay(2000);

            request = new WordbeeRequest($"trm/status?requestid={requestId}", Method.Get, Creds);
            trmResponse = await Client.ExecuteWithErrorHandling<AsyncOperationResponse>(request);

            if (trmResponse.Trm.Status == "Failed")
                throw new(trmResponse.Trm.StatusInfo);
        } while (trmResponse.Trm.Status != "Finished");

        var orderDetails = trmResponse.Result.Items.First().ToObject<OperationResultResponse<OrderOperationResponse>>();
        var orderId = orderDetails.Result.OrderDetails.OrderSummary.OrderId;

        return await GetOrder(new()
        {
            OrderId = orderId
        });
    }

    [Action("Get order", Description = "Get details of a specific order")]
    public Task<OrderEntity> GetOrder([ActionParameter] OrderRequest order)
    {
        var request = new WordbeeRequest($"orders/list/items/{order.OrderId}", Method.Get, Creds);
        request.AddHeader("Content-Type", MediaTypeNames.Application.Json);

        return Client.ExecuteWithErrorHandling<OrderEntity>(request);
    }

    [Action("Delete order", Description = "Delete specific order")]
    public Task DeleteOrder([ActionParameter] OrderRequest order)
    {
        var request = new WordbeeRequest($"orders/{order.OrderId}", Method.Delete, Creds);
        return Client.ExecuteWithErrorHandling(request);
    }
}