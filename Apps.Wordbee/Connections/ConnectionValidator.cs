using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Wordbee.Connections;

public class ConnectionValidator : WordbeeInvocable, IConnectionValidator
{
    public ConnectionValidator(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        await Client.ExecuteWithErrorHandling(new WordbeeRequest("persons/list", Method.Post, Creds));
        return new()
        {
            IsValid = true
        };
    }
}