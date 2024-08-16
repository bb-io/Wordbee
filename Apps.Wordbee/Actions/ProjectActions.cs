using System.Net.Mime;
using Apps.Wordbee.Actions.Base;
using Apps.Wordbee.Api;
using Apps.Wordbee.Models;
using Apps.Wordbee.Models.Entities;
using Apps.Wordbee.Models.Request;
using Apps.Wordbee.Models.Request.Document;
using Apps.Wordbee.Models.Request.Project;
using Apps.Wordbee.Models.Response.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.Wordbee.Actions;

[ActionList]
public class ProjectActions : WordbeeActions
{
    public ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(
        invocationContext, fileManagementClient)
    {
    }

    [Action("Search projects", Description = "Search for all projects in the workspace")]
    public async Task<ListProjectsResponse> SearchProjects()
    {
        var request = new WordbeeRequest("projects/list", Method.Post, Creds);
        var response = await Client.Paginate<ProjectEntity>(request);

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
    public async Task SubmitFile([ActionParameter] FileModel file,
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
                        token = fileResponse.Token
                    }
                },
                src = langs.SourceLanguage,
                trgs = langs.TargetLanguages,
                deadline = input.Deadline
            });

        await Client.ExecuteWithErrorHandling(request);
    }

    [Action("Download translated file", Description = "Download a translated of the workflow")]
    public async Task<FileModel> DownloadTranslatedFile([ActionParameter] ProjectDocumentRequest document)
    {
        var endpoint = $"projects/{document.ProjectId}/workflows/{document.DocumentId}/files/{document.TargetLanguage}/file";
        var request = new WordbeeRequest(endpoint, Method.Get, Creds);

        var response = await Client.ExecuteWithErrorHandling(request);

        return new()
        {
            File = await _fileManagementClient.UploadAsync(new MemoryStream(response.RawBytes),
                response.ContentType ?? MediaTypeNames.Application.Octet, document.DocumentId)
        };
    }
}