using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers;

public class LanguageDataHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    public LanguageDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var request = new WordbeeRequest("settings/languages", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<IEnumerable<LanguageEntity>>(request);

        return response
            .Where(x => string.IsNullOrWhiteSpace(context.SearchString) ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(40)
            .ToDictionary(x => x.Loc, x => x.Name);
    }
}