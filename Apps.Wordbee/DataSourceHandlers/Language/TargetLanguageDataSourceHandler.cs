using Apps.Wordbee.Models.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Wordbee.DataSourceHandlers.Language;

public class TargetLanguageDataSourceHandler : LanguageDataSourceHandler
{
    public TargetLanguageDataSourceHandler(InvocationContext invocationContext,
        [ActionParameter] LanguagesRequest documentRequest) : base(invocationContext, documentRequest.ProjectId)
    {
    }
}