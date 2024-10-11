using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers;

public class ResourceDataHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    public ResourceDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new WordbeeRequest("resources/list", Method.Post, Creds);

        var response = await Client.Paginate<ResourceEntity>(request, new
        {
            query = $"{{name}}.Contains(\"{context.SearchString}\")"
        });

        return response
            .Take(40)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}