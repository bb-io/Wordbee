using Apps.Wordbee.Models.Request.Job;
using Apps.Wordbee.Models.Request.Order;
using Apps.Wordbee.Webhooks;
using Apps.Wordbee.Webhooks.Models;
using Apps.Wordbee.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common.Polling;
using Wordbee.Base;

namespace Tests.Wordbee
{
    [TestClass]
    public class PollingTests : TestBase
    {
        [TestMethod]
        public async Task OnProjectStatusChanged_IsSuccess()
        {
            var polling = new PollingList(InvocationContext);

            var response = await polling.OnProjectStatusChanged(
                new PollingEventRequest<SingleProjectStatusMemory>
                {
                    Memory = new SingleProjectStatusMemory
                    {
                        LastCheckedUtc = DateTime.UtcNow.AddDays(-1),
                        LastKnownStatus = "0",
                        Notified= false
                    }
                }, new Apps.Wordbee.Models.Request.Project.ProjectRequest { ProjectId= "132169" },
                new SearchProjectsPollingRequest { Status="0"});

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task OnOrderStatusChanged_IsSuccess()
        {
            var polling = new PollingList(InvocationContext);

            var response = await polling.OnOrderStatusChanged(
                new PollingEventRequest<SingleOrderStatusMemory>
                {
                    Memory = new SingleOrderStatusMemory
                    {
                        LastCheckedUtc = DateTime.UtcNow.AddDays(-1),
                        LastKnownStatus = "0",
                        Notified = false
                    }
                }, new OrderRequest { OrderId= "130051" },new SearchOrdersPollingRequest { Status = "0" });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task OnJobStatusChanged_IsSuccess()
        {
            var polling = new PollingList(InvocationContext);

            var response = await polling.OnJobStatusChanged(
                new PollingEventRequest<SingleJobStatusMemory>
                {
                    Memory = new SingleJobStatusMemory
                    {
                        LastCheckedUtc = DateTime.UtcNow.AddDays(-1),
                        LastKnownStatus = "0",
                        Notified = false
                    }
                }, new JobRequest { JobId = "130051" }, new SearchJobsPollingRequest { Status = "8" });

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Assert.IsNotNull(response);
        }
    }
}
