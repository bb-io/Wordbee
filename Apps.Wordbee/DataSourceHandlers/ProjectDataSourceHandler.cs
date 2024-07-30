using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers;

public class ProjectDataSourceHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    public ProjectDataSourceHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new WordbeeRequest("projects/list", Method.Post, Creds).WithJsonBody(new
        {
            query = $"{{reference}}.Contains(\"{context.SearchString}\")"
        });
        var response = await Client.Paginate<ProjectEntity>(request);

        return response
            .Take(40)
            .ToDictionary(x => x.Id, x => x.Reference);
    }
}