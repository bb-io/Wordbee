using Apps.Wordbee.Api;
using Apps.Wordbee.Invocables;
using Apps.Wordbee.Models.Request.Job;
using Apps.Wordbee.Models.Request.Order;
using Apps.Wordbee.Models.Request.Project;
using Apps.Wordbee.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.Wordbee.Webhooks
{
    [PollingEventList]
    public class PollingList : WordbeeInvocable
    {
        public PollingList(InvocationContext invocationContext) : base(invocationContext)
        {
        }


        [PollingEvent("On project status changed", Description = "Triggers when the specified project's status equals the expected value")]
        public async Task<PollingEventResponse<SingleProjectStatusMemory, ProjectStatusReachedResponse>> OnProjectStatusChanged(
            PollingEventRequest<SingleProjectStatusMemory> request,
            [PollingEventParameter] ProjectRequest projectId,
            [PollingEventParameter] SearchProjectsPollingRequest expectedStatus)
        {
            var creds = InvocationContext.AuthenticationCredentialsProviders.ToArray();

            var project = await TryFetchSingleProject(Client, creds, projectId.ProjectId);

            if (project == null)
            {
                if (request.Memory == null)
                {
                    return new PollingEventResponse<SingleProjectStatusMemory, ProjectStatusReachedResponse>
                    {
                        FlyBird = false,
                        Memory = new SingleProjectStatusMemory { LastCheckedUtc = DateTime.UtcNow }
                    };
                }

                request.Memory.LastCheckedUtc = DateTime.UtcNow;
                return new PollingEventResponse<SingleProjectStatusMemory, ProjectStatusReachedResponse>
                {
                    FlyBird = false,
                    Memory = request.Memory
                };
            }

            if (request.Memory == null)
            {
                return new PollingEventResponse<SingleProjectStatusMemory, ProjectStatusReachedResponse>
                {
                    FlyBird = false,
                    Memory = new SingleProjectStatusMemory
                    {
                        LastCheckedUtc = DateTime.UtcNow,
                        LastKnownStatus = project.status,
                        Notified = project.status.ToString() == expectedStatus.Status
                    }
                };
            }

            var was = request.Memory.LastKnownStatus;
            request.Memory.LastKnownStatus = project.status;
            request.Memory.LastCheckedUtc = DateTime.UtcNow;

            if (project.status.ToString() == expectedStatus.Status && !request.Memory.Notified)
            {
                request.Memory.Notified = true;
                return new PollingEventResponse<SingleProjectStatusMemory, ProjectStatusReachedResponse>
                {
                    FlyBird = true,
                    Memory = request.Memory,
                    Result = new ProjectStatusReachedResponse
                    {
                        Project = new ProjectStatusReached
                        {
                            ProjectId = projectId.ProjectId,
                            Reference = project.reference,
                            Status = project.status,
                            StatusTitle = project.statust,
                            OldStatus = was,
                            Client = project.client,
                            Deadline = project.deadline,
                            SourceLangTitle = project.srct,
                            SourceLang = project.src,
                            DateReceived = project.dtreceived,
                            DateInProgress = project.dtinprogress,
                            DateCompleted = project.dtcompletion,
                            DateArchived = project.dtarchival
                        }
                    }
                };
            }

            return new PollingEventResponse<SingleProjectStatusMemory, ProjectStatusReachedResponse>
            {
                FlyBird = false,
                Memory = request.Memory
            };
        }


        [PollingEvent("On order status changed", Description = "Triggers when the specified order's status equals the expected value")]
        public async Task<PollingEventResponse<SingleOrderStatusMemory, OrderStatusReachedResponse>> OnOrderStatusChanged(
           PollingEventRequest<SingleOrderStatusMemory> request,
           [PollingEventParameter, Display("Order ID")] OrderRequest orderId,
           [PollingEventParameter, Display("Expected status")] SearchOrdersPollingRequest expectedStatus)
        {
            var creds = InvocationContext.AuthenticationCredentialsProviders.ToArray();

            var order = await TryFetchSingleOrder(Client, creds, orderId.OrderId);

            if (order == null)
            {
                if (request.Memory == null)
                {
                    return new PollingEventResponse<SingleOrderStatusMemory, OrderStatusReachedResponse>
                    {
                        FlyBird = false,
                        Memory = new SingleOrderStatusMemory { LastCheckedUtc = DateTime.UtcNow }
                    };
                }

                request.Memory.LastCheckedUtc = DateTime.UtcNow;
                return new PollingEventResponse<SingleOrderStatusMemory, OrderStatusReachedResponse>
                {
                    FlyBird = false,
                    Memory = request.Memory
                };
            }

            if (request.Memory == null)
            {
                return new PollingEventResponse<SingleOrderStatusMemory, OrderStatusReachedResponse>
                {
                    FlyBird = false,
                    Memory = new SingleOrderStatusMemory
                    {
                        LastCheckedUtc = DateTime.UtcNow,
                        LastKnownStatus = order.status,
                        Notified = string.Equals(order.status, expectedStatus.Status, StringComparison.OrdinalIgnoreCase)
                    }
                };
            }

            var was = request.Memory.LastKnownStatus;
            request.Memory.LastKnownStatus = order.status;
            request.Memory.LastCheckedUtc = DateTime.UtcNow;

            if (string.Equals(order.status, expectedStatus.Status, StringComparison.OrdinalIgnoreCase)
                && !request.Memory.Notified)
            {
                request.Memory.Notified = true;

                return new PollingEventResponse<SingleOrderStatusMemory, OrderStatusReachedResponse>
                {
                    FlyBird = true,
                    Memory = request.Memory,
                    Result = new OrderStatusReachedResponse
                    {
                        Order = new OrderStatusReached
                        {
                            OrderId = orderId.OrderId,
                            Reference = order.reference,
                            Status = order.status,
                            StatusTitle = order.statust,
                            OldStatus = was,
                            InternalId = order.id,
                            CompanyName = order.companyName,
                            CompanyId = order.companyId,
                            PersonName = order.personName,
                            PersonId = order.personId,
                            SourceLangTitle = order.srct,
                            SourceLang = order.src,
                            TargetLang = order.trg,
                            TargetLangTitle = order.trgt,
                            Created = order.created,
                            DateReceived = order.dtreceived,
                            Deadline = order.deadline,
                            ProjectId = order.projectId,
                            ProjectManagerId = order.projectManagerId,
                            ProjectResourceId = order.projectResourceId,
                            ProjectStatusTitle = order.projectStatust
                        }
                    }
                };
            }

            return new PollingEventResponse<SingleOrderStatusMemory, OrderStatusReachedResponse>
            {
                FlyBird = false,
                Memory = request.Memory
            };
        }


        [PollingEvent("On job status changed", Description = "Triggers when the specified job's status equals the expected value")]
        public async Task<PollingEventResponse<SingleJobStatusMemory, JobStatusReachedResponse>> OnJobStatusChanged(
            PollingEventRequest<SingleJobStatusMemory> request,
            [PollingEventParameter, Display("Job ID")] JobRequest jobId,
            [PollingEventParameter, Display("Expected status")] SearchJobsPollingRequest expectedStatus)
        {
            var creds = InvocationContext.AuthenticationCredentialsProviders.ToArray();

            var job = await TryFetchSingleJob(Client, creds, jobId.JobId);
            if (job == null)
            {
                if (request.Memory == null)
                {
                    return new PollingEventResponse<SingleJobStatusMemory, JobStatusReachedResponse>
                    {
                        FlyBird = false,
                        Memory = new SingleJobStatusMemory { LastCheckedUtc = DateTime.UtcNow }
                    };
                }

                request.Memory.LastCheckedUtc = DateTime.UtcNow;
                return new PollingEventResponse<SingleJobStatusMemory, JobStatusReachedResponse>
                {
                    FlyBird = false,
                    Memory = request.Memory
                };
            }

            if (request.Memory == null)
            {
                return new PollingEventResponse<SingleJobStatusMemory, JobStatusReachedResponse>
                {
                    FlyBird = false,
                    Memory = new SingleJobStatusMemory
                    {
                        LastCheckedUtc = DateTime.UtcNow,
                        LastKnownStatus = job.status,
                        Notified = string.Equals(job.status, expectedStatus.Status, StringComparison.OrdinalIgnoreCase)
                    }
                };
            }

            var was = request.Memory.LastKnownStatus;
            request.Memory.LastKnownStatus = job.status;
            request.Memory.LastCheckedUtc = DateTime.UtcNow;

            if (string.Equals(job.status, expectedStatus.Status, StringComparison.OrdinalIgnoreCase)
                && !request.Memory.Notified)
            {
                request.Memory.Notified = true;

                return new PollingEventResponse<SingleJobStatusMemory, JobStatusReachedResponse>
                {
                    FlyBird = true,
                    Memory = request.Memory,
                    Result = new JobStatusReachedResponse
                    {
                        Job = new JobStatusReached
                        {
                            JobId = job.jobid ?? job.id ?? jobId.JobId,
                            InternalId = job.id,
                            Reference = job.reference,
                            Status = job.status,
                            StatusTitle = job.statust,
                            OldStatus = was,
                            Deadline = job.deadline,
                            AssignmentMode = job.amode,
                            AssignmentModeTitle = job.amodet,
                            CompanyId = job.cid,
                            CompanyName = job.cname,
                            UserId = job.uid,
                            UserName = job.uname,
                            Task = job.task,
                            TaskTitle = job.taskt,
                            SourceLang = job.src,
                            SourceLangTitle = job.srct,
                            TargetLang = job.trg,
                            TargetLangTitle = job.trgt,
                            Branch = job.branch,
                            ProjectId = job.pid
                        }
                    }
                };
            }

            return new PollingEventResponse<SingleJobStatusMemory, JobStatusReachedResponse>
            {
                FlyBird = false,
                Memory = request.Memory
            };
        }

        private static async Task<JobListItem?> TryFetchSingleJob(
            WordbeeClient client,
            IEnumerable<AuthenticationCredentialsProvider> creds,
            string jobId)
        {
            var rhs = int.TryParse(jobId, out _) ? jobId : $"\"{jobId.Replace("\"", "\\\"")}\"";
            var query = $@"{{jobid}} = {rhs}";

            var req = new WordbeeRequest("jobs/list", Method.Post, creds);
            var payload = new { query };

            var rows = await client.Paginate<JobListItem>(req, payload);
            return rows?.FirstOrDefault();
        }

        private static async Task<OrderListItem?> TryFetchSingleOrder(
          WordbeeClient client,
          IEnumerable<AuthenticationCredentialsProvider> creds,
          string orderId)
        {
            var rhs = int.TryParse(orderId, out _) ? orderId : $"\"{orderId.Replace("\"", "\\\"")}\"";
            var query = $@"{{id}} = {rhs}";

            var req = new WordbeeRequest("orders/list", Method.Post, creds);
            var payload = new { query };

            var rows = await client.Paginate<OrderListItem>(req, payload);
            return rows?.FirstOrDefault();
        }

        private static async Task<ProjectListItem?> TryFetchSingleProject(
             WordbeeClient client,
             IEnumerable<AuthenticationCredentialsProvider> creds,
             string projectId)
        {
            var rhs = int.TryParse(projectId, out _) ? projectId : $"\"{projectId.Replace("\"", "\\\"")}\"";
            var query = $@"{{id}} = {rhs}";

            var req = new WordbeeRequest("projects/list", Method.Post, creds);
            var payload = new { query };

            var rows = await client.Paginate<ProjectListItem>(req, payload);
            return rows?.FirstOrDefault();
        }
    }
}
