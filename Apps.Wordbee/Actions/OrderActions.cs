using System.Net.Mime;
using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Apps.Wordbee.Models.Request.Order;
using Apps.Wordbee.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
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
}