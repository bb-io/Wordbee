using Apps.Wordbee.Models.Request.Document;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Wordbee.DataSourceHandlers.Language;

public class DocumentLanguageDataSourceHandler : LanguageDataSourceHandler
{
    public DocumentLanguageDataSourceHandler(InvocationContext invocationContext,
        [ActionParameter] ProjectDocumentRequest documentRequest) : base(invocationContext, documentRequest.ProjectId)
    {
    }
}