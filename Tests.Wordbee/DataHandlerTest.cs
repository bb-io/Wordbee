using Apps.Wordbee.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Wordbee.Base;

namespace Tests.Wordbee
{
    [TestClass]
    public class DataHandlerTest : TestBase
    {
        [TestMethod]
        public async Task LanguageDataHandler_ReturnsValue()
        {
            var action = new GlobalLanguageDataHandler(InvocationContext);

            var response = await action.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

            foreach (var item in response)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
                Assert.IsNotNull(item);
            }
        }
    }
}
