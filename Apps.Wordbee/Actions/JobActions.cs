using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Apps.Wordbee.Models.Request.Job;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.Actions;

[ActionList]
public class JobActions : WordbeeInvocable
{
    public JobActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Get job", Description = "Get details of a specific job")]
    public Task<JobEntity> GetJob([ActionParameter] JobRequest project)
    {
        var request = new WordbeeRequest($"jobs/list/items/{project.JobId}", Method.Post, Creds);
        return Client.ExecuteWithErrorHandling<JobEntity>(request);
    }
}