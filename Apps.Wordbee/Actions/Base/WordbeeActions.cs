using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Response;
using Apps.Wordbee.Models.Response.File;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Wordbee.Actions.Base;

public class WordbeeActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : WordbeeInvocable(invocationContext)
{
    protected async Task<UploadFileResponse> UploadFile(FileReference file)
    {
        var originalStream = await fileManagementClient.DownloadAsync(file);
        if (originalStream == null)
        {
            throw new PluginMisconfigurationException("Failed to download file. Please check the file source.");
        }

        var memoryStream = new MemoryStream();
        await originalStream.CopyToAsync(memoryStream);

        memoryStream.Position = 0;

        if (memoryStream.Length == 0)
        {
            throw new PluginMisconfigurationException("The file is empty. Please check and provide a valid file.");
        }

        var request = new WordbeeRequest("media/upload", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        };

        request.AddFile("file", () =>
        {
            var s = new MemoryStream(memoryStream.ToArray());
            return s;
        }, file.Name);

        var response = await Client.ExecuteWithErrorHandling<ResultResponse<UploadFileResponse>>(request);
        return response.Result;
    }

    protected async Task<T> GetAsyncOperationResult<T>(string requestId) where T : AsyncOperationResponse
    {
        if (string.IsNullOrWhiteSpace(requestId))
            throw new PluginApplicationException($"Wordbee returned empty request id. Please try again later");

        T trmResponse;
        do
        {
            await Task.Delay(2000);

            var request = new WordbeeRequest($"trm/status?requestid={requestId}", Method.Get, Creds);
            trmResponse = await Client.ExecuteWithErrorHandling<T>(request);

            if (trmResponse.Trm is null)
            {
                throw new PluginApplicationException("Wordbee async operation returned  null/empty response.");
            }

            if (string.Equals(trmResponse.Trm.Status, "Failed", StringComparison.OrdinalIgnoreCase))
            {
                var status = trmResponse.Trm.Status;
                var statusText = trmResponse.Trm.StatusText;
                var statusInfo = trmResponse.Trm.StatusInfo;

                var message =
                    $"Wordbee async operation failed. " +
                    (string.IsNullOrWhiteSpace(status) ? "" : $"Status: {status}. ") +
                    (string.IsNullOrWhiteSpace(statusText) ? "" : $"Status text: {statusText}. ") +
                    (string.IsNullOrWhiteSpace(statusInfo) ? "" : $"Info: {statusInfo}.");

                if (string.IsNullOrWhiteSpace(message))
                    message = "Wordbee async operation failed without providing futher details. Please try again later";

                throw new PluginApplicationException(message);
            }

        } while (!string.Equals(trmResponse.Trm.Status, "Finished", StringComparison.OrdinalIgnoreCase));

        return trmResponse;
    }
}