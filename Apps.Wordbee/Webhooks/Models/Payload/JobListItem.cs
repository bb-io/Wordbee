using Apps.Wordbee.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Wordbee.Webhooks.Models.Payload
{
    public class JobListItem
    {
        public string? id { get; set; }         
        public string? jobid { get; set; }      
        public string status { get; set; }
        public string? statust { get; set; }
        public string? reference { get; set; }
        public DateTime? deadline { get; set; }

        public string? amode { get; set; }      
        public string? amodet { get; set; }    

        public string? cid { get; set; }
        public string? cname { get; set; }
        public string? uid { get; set; }
        public string? uname { get; set; }

        public string? task { get; set; }       
        public string? taskt { get; set; }      

        public string? src { get; set; }
        public string? trg { get; set; }
        public string? srct { get; set; }
        public string? trgt { get; set; }

        public string? branch { get; set; }
        public string? pid { get; set; }       
    }

    public class SingleJobStatusMemory
    {
        public DateTime LastCheckedUtc { get; set; }
        public string? LastKnownStatus { get; set; }
        public bool Notified { get; set; }
    }

    public class JobStatusReached
    {
        [Display("Job ID")]
        public string JobId { get; set; }    

        [Display("Internal ID")]
        public string? InternalId { get; set; }     

        [Display("Reference")]
        public string? Reference { get; set; }

        [Display("Status")]
        public string Status { get; set; }

        [Display("Status Title")]
        public string? StatusTitle { get; set; }

        [Display("Old Status")]
        public string? OldStatus { get; set; }

        [Display("Deadline")]
        public DateTime? Deadline { get; set; }

        [Display("Assignment mode")]
        public string? AssignmentMode { get; set; }          
        [Display("Assignment mode title")]
        public string? AssignmentModeTitle { get; set; }    

        [Display("Company ID")]
        public string? CompanyId { get; set; }              
        [Display("Company name")]
        public string? CompanyName { get; set; }          

        [Display("User ID")]
        public string? UserId { get; set; }                 
        [Display("User name")]
        public string? UserName { get; set; }                

        [Display("Task code")]
        public string? Task { get; set; }                   
        [Display("Task title")]
        public string? TaskTitle { get; set; }               

        [Display("Source language")]
        public string? SourceLang { get; set; }             
        [Display("Source language title")]
        public string? SourceLangTitle { get; set; }      
        [Display("Target language")]
        public string? TargetLang { get; set; }           
        [Display("Target language title")]
        public string? TargetLangTitle { get; set; }        

        [Display("Branch")]
        public string? Branch { get; set; }

        [Display("Project ID")]
        public string? ProjectId { get; set; }             
    }

    public class JobStatusReachedResponse
    {
        public JobStatusReached Job { get; set; } = new();
    }

    public class SearchJobsPollingRequest
    {
        [StaticDataSource(typeof(JobStatusStaticHandler))]
        public string? Status { get; set; }
    }
}
