using Apps.Wordbee.DataSourceHandlers;
using Apps.Wordbee.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Wordbee.Webhooks.Models.Input;

public class OrderChangedWebhookInput
{
    [Display("Order ID")]
    [DataSource(typeof(OrderDataSourceHandler))]
    public string? OrderId { get; set; }

    [Display("User ID")]
    [DataSource(typeof(UserDataHandler))]
    public string? UserId { get; set; }

    [Display("Project ID")]
    [DataSource(typeof(ProjectDataSourceHandler))]
    public string? ProjectId { get; set; }

    [Display("Client user ID")]
    public string? ClientUserId { get; set; }

    [Display("Order status")]
    [StaticDataSource(typeof(OrderStatusStaticHandler))]
    public string? Status { get; set; }
}