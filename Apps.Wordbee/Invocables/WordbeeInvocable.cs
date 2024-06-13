using Apps.Wordbee.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Wordbee.Invocables;

public class WordbeeInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected WordbeeClient Client { get; }
    public WordbeeInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
        Client.SetToken().Wait();
    }
}