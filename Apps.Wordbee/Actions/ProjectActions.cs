using System.Net.Mime;
using System.Text;
using Apps.Wordbee.Actions.Base;
using Apps.Wordbee.Api;
using Apps.Wordbee.Models;
using Apps.Wordbee.Models.Entities;
using Apps.Wordbee.Models.Request;
using Apps.Wordbee.Models.Request.Document;
using Apps.Wordbee.Models.Request.Project;
using Apps.Wordbee.Models.Response;
using Apps.Wordbee.Models.Response.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Wordbee.Actions;

[ActionList("Projects")]
public class ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : 
    WordbeeActions(invocationContext, fileManagementClient)
{
    [Action("Search projects", Description = "Search for all projects in the workspace")]
    public async Task<ListProjectsResponse> SearchProjects([ActionParameter] SearchProjectsRequest input)
    {
        var query = new StringBuilder();

        if (input.Status != null)
            query.Append($"{{status}} = {input.Status}");

        var request = new WordbeeRequest("projects/list", Method.Post, Creds);
        var response = await Client.Paginate<ProjectEntity>(request, new
        {
            query = query.ToString()
        });

        return new()
        {
            Projects = response
        };
    }

    [Action("Get project", Description = "Get details of a specific project")]
    public Task<ProjectEntity> GetProject([ActionParameter] ProjectRequest project)
    {
        var request = new WordbeeRequest($"projects/list/items/{project.ProjectId}", Method.Get, Creds);
        request.AddHeader("Content-Type", MediaTypeNames.Application.Json);

        return Client.ExecuteWithErrorHandling<ProjectEntity>(request);
    }

    [Action("Submit new file", Description = "Add new file to an existing project")]
    public async Task<DocumentEntity> SubmitFile([ActionParameter] FileModel file,
        [ActionParameter] SubmitNewProjectFileInput input,
        [ActionParameter] LanguagesRequest langs)
    {
        var fileResponse = await UploadFile(file.File);

        var request = new WordbeeRequest($"projects/{langs.ProjectId}/workflows/new", Method.Post, Creds)
            .AddJsonBody(new
            {
                files = new[]
                {
                    new
                    {
                        name = file.File.Name,
                        token = fileResponse.Token,
                        disableMt = input.DisableMt ?? false,
                        reference = input.Reference,
                        comments = input.Comments
                    }
                },
                src = langs.SourceLanguage,
                trgs = langs.TargetLanguages,
                deadline = input.Deadline
            });

        var response = await Client.ExecuteWithErrorHandling<AsyncOperationResponse>(request);
        var operationResult = await GetAsyncOperationResult<SubmitFileAsyncOperationResponse>(response.Trm.RequestId);

        return operationResult.Custom.Files.First();
    }

    [Action("Confirm delivery", Description = "Confirm delivery of a specific document")]
    public Task<ConfirmDocumentDeliveryResponse> ConfirmDelivery([ActionParameter] ProjectDocumentRequest input)
    {
        var endpoint =
            $"/projects/{input.ProjectId}/workflows/{input.DocumentId}/status/{input.TargetLanguage}/delivery/success";
        var request = new WordbeeRequest(endpoint, Method.Post, Creds);

        return Client.ExecuteWithErrorHandling<ConfirmDocumentDeliveryResponse>(request);
    }

    [Action("Download translated file", Description = "Download a translated of the workflow")]
    public async Task<FileModel> DownloadTranslatedFile([ActionParameter] ProjectDocumentRequest document)
    {
        var endpoint =
            $"projects/{document.ProjectId}/workflows/{document.DocumentId}/files/{document.TargetLanguage}/file";
        var request = new WordbeeRequest(endpoint, Method.Get, Creds);

        var response = await Client.ExecuteWithErrorHandling(request);

        if (response.RawBytes == null || response.RawBytes.Length == 0)
        {
            throw new PluginApplicationException("The downloaded file is empty. Please check and try again.");
        }

        return new()
        {
            File = await fileManagementClient.UploadAsync(new MemoryStream(response.RawBytes),
                response.ContentType ?? MediaTypeNames.Application.Octet, document.DocumentId)
        };
    }
}