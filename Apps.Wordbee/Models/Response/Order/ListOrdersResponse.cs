using Apps.Wordbee.Models.Entities;

namespace Apps.Wordbee.Models.Response.Order;

public class ListOrdersResponse
{
    public IEnumerable<OrderEntity> Orders { get; set; }
}