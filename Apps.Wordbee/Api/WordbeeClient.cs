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
        BaseUrl = "https://www.wordbee-translator.com/api".ToUri()
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

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        var token = await GetToken();

        this.AddDefaultHeader("X-Auth-Token", token);
        return await base.ExecuteWithErrorHandling(request);
    }

    private async Task<string> GetToken()
    {
        var request = new RestRequest("https://www.wordbee-translator.com/api/auth/token")
            .WithJsonBody(new
            {
                accountId = _creds.Get(CredsNames.AccountId).Value,
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