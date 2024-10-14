using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Models.Request.Document;

public class ProjectDocumentOptionalRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataSourceHandler))]
    public string? ProjectId { get; set; }
    
    [Display("Document ID")] 
    [DataSource(typeof(ProjectDocumentOptionalDataHandler))]
    public string? DocumentId { get; set; }
}