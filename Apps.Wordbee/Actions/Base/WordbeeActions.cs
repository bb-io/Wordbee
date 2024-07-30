using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Response;
using Apps.Wordbee.Models.Response.File;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Wordbee.Actions.Base;

public class WordbeeActions : WordbeeInvocable
{
    protected readonly IFileManagementClient _fileManagementClient;

    public WordbeeActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    protected async Task<UploadFileResponse> UploadFile(FileReference file)
    {
        var fileStream = await _fileManagementClient.DownloadAsync(file);
        
        var request = new WordbeeRequest("media/upload", Method.Post, Creds)
        {
            AlwaysMultipartFormData = true
        };
        request.AddFile("file", () => fileStream, file.Name);

        var response = await Client.ExecuteWithErrorHandling<ResultResponse<UploadFileResponse>>(request);
        return response.Result;
    }
}