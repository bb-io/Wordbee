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

public class WordbeeClient(AuthenticationCredentialsProvider[] creds) : BlackBirdRestClient(new()
{
    BaseUrl = creds.Get(CredsNames.Url).Value.ToUri()
})
{
    private const int PaginationLimit = 200;

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
                accountid = creds.Get(CredsNames.AccountId).Value,
                key = creds.Get(CredsNames.ApiKey).Value,
            });

        var response = await ExecuteAsync(request);
        return response.Content!;
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        string statusPart = $"Status code {response.StatusCode}.";
        
        if (string.IsNullOrWhiteSpace(response.Content))
            return new PluginApplicationException($"{statusPart} Server did not return any content.");

        if (response.ContentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) == true)
        {
            try
            {
                var jsonError = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                return jsonError?.Reason != null
                    ? new PluginApplicationException(jsonError.Reason)
                    : new PluginApplicationException($"{statusPart} Unknown error. Raw: {response.Content}");
            }
            catch (JsonException)
            {
                return new PluginApplicationException($"{statusPart} Invalid JSON error format. Raw: {response.Content}");
            }
        }

        string rawMessage = response.Content.Substring(0, Math.Min(response.Content.Length, 300));
        return new PluginMisconfigurationException($"{statusPart} Raw Content: {rawMessage}");
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        var response = await ExecuteWithErrorHandling(request);
        string content = response.Content!;
        
        T? val = JsonConvert.DeserializeObject<T>(content, JsonSettings) ??
                 throw new Exception($"Could not parse {content} to {typeof(T)}");
        
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