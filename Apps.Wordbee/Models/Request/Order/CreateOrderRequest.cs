using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Models.Request.Order;

public class CreateOrderRequest
{
    [Display("Company ID"), DataSource(typeof(CompanyDataSourceHandler))]
    public string CompanyId { get; set; }

    public string Reference { get; set; }

    [Display("Source languages"), DataSource(typeof(GlobalLanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target languages"), DataSource(typeof(GlobalLanguageDataHandler))]
    public IEnumerable<string> TargetLanguages { get; set; }

    public DateTime? Deadline { get; set; }
}