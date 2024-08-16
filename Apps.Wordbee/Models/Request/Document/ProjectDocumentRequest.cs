using Apps.Wordbee.DataSourceHandlers;
using Apps.Wordbee.DataSourceHandlers.Language;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Models.Request.Document;

public class ProjectDocumentRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataSourceHandler))]
    public string ProjectId { get; set; }
    
    [Display("Document ID")] 
    [DataSource(typeof(ProjectDocumentDataHandler))]
    public string DocumentId { get; set; }
    
    [Display("Target language")]
    [DataSource(typeof(DocumentLanguageDataSourceHandler))]
    public string TargetLanguage { get; set; }
}