using Apps.Wordbee.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.Wordbee.Api;

public class WordbeeRequest : BlackBirdRestRequest
{
    public WordbeeRequest(string resource, Method method, IEnumerable<AuthenticationCredentialsProvider> creds) : base(resource, method, creds)
    {
    }

    protected override void AddAuth(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        this.AddHeader("X-Auth-AccountId", creds.Get(CredsNames.AccountId).Value);
    }
}