using Apps.Wordbee.DataSourceHandlers;
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
}