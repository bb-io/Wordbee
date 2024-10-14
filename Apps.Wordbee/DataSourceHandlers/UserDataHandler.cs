using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers;

public class UserDataHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    public UserDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new WordbeeRequest("persons/list", Method.Post, Creds);

        var response = await Client.Paginate<UserEntity>(request, new
        {
            query =
                $"{{firstname}}.Contains(\"{context.SearchString}\") OR {{lastname}}.Contains(\"{context.SearchString}\")"
        });

        return response
            .Take(40)
            .ToDictionary(x => x.PersonId, x => x.DisplayName);
    }
}