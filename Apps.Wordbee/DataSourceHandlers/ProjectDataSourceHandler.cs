using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers;

public class ProjectDataSourceHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    public ProjectDataSourceHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    // TODO: Add filtering by name via API
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new WordbeeRequest("projects/list", Method.Post, Creds);
        var response = await Client.Paginate<ProjectEntity>(request);

        return response
            .Where(x => string.IsNullOrWhiteSpace(context.SearchString) ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(40)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}