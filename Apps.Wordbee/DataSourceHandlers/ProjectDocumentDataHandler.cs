using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Entities;
using Apps.Wordbee.Models.Request.Document;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.DataSourceHandlers;

public class ProjectDocumentDataHandler : WordbeeInvocable, IAsyncDataSourceHandler
{
    private readonly ProjectDocumentRequest _request;

    public ProjectDocumentDataHandler(InvocationContext invocationContext,
        [ActionParameter] ProjectDocumentRequest request) : base(invocationContext)
    {
        _request = request;
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_request.ProjectId))
            throw new("You should input Project ID first");

        var request = new WordbeeRequest("/resources/documents/list", Method.Post, Creds);
        var response = await Client.Paginate<DocumentEntity>(request, new
        {
            scope = new
            {
                type = "Project",
                projectid = long.Parse(_request.ProjectId)
            }
        });

        return response
            .Where(x => context.SearchString is null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(40)
            .ToDictionary(x => x.Did, x => x.Name);
    }
}