using Apps.Wordbee.Constants;
using Apps.Wordbee.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apps.Wordbee.Api;

public class WordbeeClient : BlackBirdRestClient
{
    private const int PaginationLimit = 200;
    private readonly AuthenticationCredentialsProvider[] _creds;

    public WordbeeClient(AuthenticationCredentialsProvider[] creds) : base(new()
    {
        BaseUrl = creds.Get(CredsNames.Url).Value.ToUri()
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

    public async Task<List<T>> Paginate<T>(RestRequest request, object payload)
    {
        var jObjectPayload = JObject.FromObject(payload);
        var offset = 0;

        var result = new List<T>();
        PaginationResponse<T> response;
        do
        {
            jObjectPayload["skip"] = offset;
            jObjectPayload["take"] = PaginationLimit;
            var jsonPayload = JsonConvert.SerializeObject(jObjectPayload);

            request.AddJsonBody(jsonPayload, false);
            response = await ExecuteWithErrorHandling<PaginationResponse<T>>(request);
            result.AddRange(response.Rows ?? response.Items ?? []);

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
        return new PluginApplicationException(error.Reason);
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        string content = (await ExecuteWithErrorHandling(request)).Content;
        T val = JsonConvert.DeserializeObject<T>(content, JsonSettings);
        if (val == null)
        {
            throw new Exception($"Could not parse {content} to {typeof(T)}");
        }

        return val;
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        RestResponse restResponse = await ExecuteAsync(request);
        if (!restResponse.IsSuccessStatusCode)
        {
            throw ConfigureErrorException(restResponse);
        }

        return restResponse;
    }
}