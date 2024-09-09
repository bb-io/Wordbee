using Apps.Wordbee.DataSourceHandlers;
using Apps.Wordbee.DataSourceHandlers.Language;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Models.Request;

public class LanguagesRequest
{
    [Display("Project ID")]
    [DataSource(typeof(ProjectDataSourceHandler))]
    public string ProjectId { get; set; }
    
    [Display("Source language")]
    [DataSource(typeof(SourceLanguageDataSourceHandler))]
    public string SourceLanguage { get; set; }
    
    [Display("Target languages")]
    [DataSource(typeof(TargetLanguageDataSourceHandler))]
    public IEnumerable<string> TargetLanguages { get; set; }
}