using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Models.Request.Order;

public class OrderRequest
{
    [Display("Order ID")]
    [DataSource(typeof(OrderDataSourceHandler))]
    public string OrderId { get; set; }
}