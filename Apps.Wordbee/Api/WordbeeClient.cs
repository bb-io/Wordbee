using Apps.Wordbee.Constants;
using Apps.Wordbee.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Wordbee.Api;

public class WordbeeClient : BlackBirdRestClient
{
    private const int PaginationLimit = 500;
    private readonly AuthenticationCredentialsProvider[] _creds;

    public WordbeeClient(AuthenticationCredentialsProvider[] creds) : base(new()
    {
        BaseUrl = (creds.Get(CredsNames.Url).Value.Trim('/') + "/api").ToUri()
    })
    {
        _creds = creds;
    }

    public async Task<List<T>> Paginate<T>(RestRequest request)
    {
        var baseUrl = request.Resource;
        var offset = 0;
        
        var result = new List<T>();
        PaginationResponse<T> response;
        do
        {
            request.Resource = baseUrl
                .SetQueryParameter("skip", offset.ToString())
                .SetQueryParameter("take", PaginationLimit.ToString());
            
            response = await ExecuteWithErrorHandling<PaginationResponse<T>>(request);
            result.AddRange(response.Rows);

            offset += PaginationLimit;
        } while (response.Total > result.Count);

        return result;
    }

    public async Task SetToken()
    {
        var token = await GetToken();
        this.AddDefaultHeader("X-Auth-Token", token.Trim('"'));
    }

    private async Task<string> GetToken()
    {
        var request = new RestRequest("/auth/token", Method.Post)
            .WithJsonBody(new
            {
                accountid = _creds.Get(CredsNames.AccountId).Value,
                key = _creds.Get(CredsNames.ApiKey).Value,
            });

        var response = await ExecuteAsync(request);
        return response.Content!;
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content!)!;
        return new(error.Reason);
    }
}