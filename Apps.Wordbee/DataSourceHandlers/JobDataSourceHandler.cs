using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers;

public class JobDataSourceHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    public JobDataSourceHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new WordbeeRequest("jobs/list", Method.Post, Creds);
        var response = await Client.Paginate<JobEntity>(request, new
        {
            query = $"{{reference}}.Contains(\"{context.SearchString}\")"
        });

        return response
            .Take(40)
            .ToDictionary(x => x.Id, x => x.Reference);
    }
}