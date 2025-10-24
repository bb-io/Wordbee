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
        var fileStream = await fileManagementClient.DownloadAsync(file);

        int firstByte = fileStream.ReadByte();

        if (fileStream == null)
        {
            throw new PluginMisconfigurationException("Failed to download file. Please check the file source.");
        }

        if (firstByte == -1)
        {
            throw new PluginMisconfigurationException("The file is empty. Please check and provide a valid file.");
        }
        fileStream.Position = 0;

        var request = new WordbeeRequest("media/upload", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        };
        request.AddFile("file", () => fileStream, file.Name);

        var response = await Client.ExecuteWithErrorHandling<ResultResponse<UploadFileResponse>>(request);
        return response.Result;
    }

    protected async Task<T> GetAsyncOperationResult<T>(string requestId) where T : AsyncOperationResponse
    {
        T trmResponse;
        do
        {
            await Task.Delay(2000);

            var request = new WordbeeRequest($"trm/status?requestid={requestId}", Method.Get, Creds);
            trmResponse = await Client.ExecuteWithErrorHandling<T>(request);

            if (trmResponse.Trm.Status == "Failed")
                throw new(trmResponse.Trm?.StatusInfo ?? trmResponse.Trm?.Status + " " + trmResponse.Trm?.StatusText);
        } while (trmResponse.Trm.Status != "Finished");

        return trmResponse;
    }
}