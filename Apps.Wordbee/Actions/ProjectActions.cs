using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Apps.Wordbee.Models.Request.Project;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.Actions;

[ActionList]
public class ProjectActions : WordbeeInvocable
{
    public ProjectActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get project", Description = "Get details of a specific project")]
    public Task<ProjectEntity> GetProject([ActionParameter] ProjectRequest project)
    {
        var request = new WordbeeRequest($"projects/list/items/{project.ProjectId}", Method.Post, Creds);
        return Client.ExecuteWithErrorHandling<ProjectEntity>(request);
    }
}