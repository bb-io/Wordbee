using Apps.Wordbee.Actions;
using Apps.Wordbee.Models.Request.Order;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Wordbee.Base;

namespace Tests.Wordbee
{
    [TestClass]
    public class DataSources : TestBase
    {
        [TestMethod]
        public async Task CreateOrder_ReturnsSuccess()
        {
            var action = new OrderActions(InvocationContext, FileManager);

            var input = new CreateOrderRequest
            {

            };

                var result = await action.CreateOrder(input, CancellationToken.None);
        }


    }
}
