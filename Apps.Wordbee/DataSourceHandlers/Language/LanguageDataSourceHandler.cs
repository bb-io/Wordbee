using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers.Language;

public class LanguageDataSourceHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    private readonly string _projectId;
    private readonly bool _srcOnly;

    public LanguageDataSourceHandler(InvocationContext invocationContext, string projectId, bool srcOnly = false) : base(invocationContext)
    {
        _projectId = projectId;
        _srcOnly = srcOnly;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_projectId))
            throw new("You should specify Project ID first");
        
        var request = new WordbeeRequest($"projects/{_projectId}/locales", Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<IEnumerable<LanguageEntity>>(request);

        return response
            .Where(x => string.IsNullOrWhiteSpace(context.SearchString) ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Where(x => !_srcOnly || x.IsSourceLanguage == _srcOnly)
            .Take(40)
            .ToDictionary(x => x.Code, x => x.Name);
    }
}