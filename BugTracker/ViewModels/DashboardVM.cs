using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class DashboardVM
    {
        public string FullName { get; set; }
        public List<Project> AssignedProjects { get; set; }
        public string Tickets { get; set; }
        public int SubmittedTicketCount { get; set; }
        public int AssignedTicketCount { get; set; }
        public int AssignedProjectCount { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Type { get; set; }
        public string Projects { get; set; }
        public int HighPriorityTicketCount { get; set; }
        public int UnassignedTicketCount { get; set; }
        public int RecentResolvedCount { get; set; }
    }
}