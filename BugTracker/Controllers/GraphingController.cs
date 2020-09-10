using BugTracker.Helpers;
using BugTracker.Models;
using BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Bug_Tracker.Controllers
{
    public class GraphingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();
        private UserRoleHelper userRoleHelper = new UserRoleHelper();
        private TicketHelper ticketHelper = new TicketHelper();
        public class MorrisBarData
        {
            public string label { get; set; }
            public int value { get; set; }
        }
        public JsonResult ProduceChart1Data()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            var userRole = userRoleHelper.ListUserRoles(user.Id).FirstOrDefault();
            switch (userRole)
            {
                case "Submitter":
                case "DemoSubmitter":
                    foreach (var priority in db.TicketPriorities.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = priority.Name;
                        data.value = db.Tickets.Where(t => t.TicketPriority.Name == priority.Name).Where(t => t.SubmitterId == user.Id).Count();
                        myData.Add(data);
                    }
                    break;
                case "Developer":
                case "DemoDeveloper":
                    foreach (var priority in db.TicketPriorities.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = priority.Name;
                        data.value = db.Tickets.Where(t => t.TicketPriority.Name == priority.Name).Where(t => t.DeveloperId == user.Id).Count();
                        myData.Add(data);
                    }
                    break;
                case "ProjectManager":
                case "DemoProjectManager":
                    var myTickets = ticketHelper.GetMyTickets();
                    foreach (var priority in db.TicketPriorities.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = priority.Name;
                        data.value = myTickets.Where(t => t.TicketPriority.Name == priority.Name).Count();
                        myData.Add(data);
                    }
                    break;
                case "Admin":
                case "DemoAdmin":
                    foreach (var priority in db.TicketPriorities.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = priority.Name;
                        data.value = db.Tickets.Where(t => t.TicketPriority.Name == priority.Name).Count();
                        myData.Add(data);
                    }
                    break;
            }
            return Json(myData);
        }
        public JsonResult ProduceChart2Data()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            var userRole = userRoleHelper.ListUserRoles(user.Id).FirstOrDefault();
            switch (userRole)
            {
                case "Submitter":
                case "DemoSubmitter":
                    foreach (var status in db.TicketStatuses.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = status.Name;
                        data.value = db.Tickets.Where(t => t.TicketPriority.Name == status.Name).Where(t => t.SubmitterId == user.Id).Count();
                        myData.Add(data);
                    }
                    break;
                case "Developer":
                case "DemoDeveloper":
                    foreach (var status in db.TicketStatuses.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = status.Name;
                        data.value = db.Tickets.Where(t => t.TicketStatus.Name == status.Name).Where(t => t.DeveloperId == user.Id).Count();
                        myData.Add(data);
                    }
                    break;
                case "ProjectManager":
                case "DemoProjectManager":
                    var myTickets = ticketHelper.GetMyTickets();
                    foreach (var status in db.TicketStatuses.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = status.Name;
                        data.value = myTickets.Where(t => t.TicketStatus.Name == status.Name).Count();
                        myData.Add(data);
                    }
                    break;
                case "Admin":
                case "DemoAdmin":
                    foreach (var status in db.TicketStatuses.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = status.Name;
                        data.value = db.Tickets.Where(t => t.TicketStatus.Name == status.Name).Count();
                        myData.Add(data);
                    }
                    break;
            }
            return Json(myData);
        }
        public JsonResult ProduceChart3Data()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            var userRole = userRoleHelper.ListUserRoles(user.Id).FirstOrDefault();
            switch (userRole)
            {
                case "Submitter":
                case "DemoSubmitter":
                    foreach (var type in db.TicketTypes.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = type.Name;
                        data.value = db.Tickets.Where(t => t.TicketType.Name == type.Name).Where(t => t.SubmitterId == user.Id).Count();
                        myData.Add(data);
                    }
                    break;
                case "Developer":
                case "DemoDeveloper":
                    foreach (var type in db.TicketTypes.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = type.Name;
                        data.value = db.Tickets.Where(t => t.TicketType.Name == type.Name).Where(t => t.DeveloperId == user.Id).Count();
                        myData.Add(data);
                    }
                    break;
                case "ProjectManager":
                case "DemoProjectManager":
                    var myTickets = ticketHelper.GetMyTickets();
                    foreach (var type in db.TicketTypes.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = type.Name;
                        data.value = myTickets.Where(t => t.TicketType.Name == type.Name).Count();
                        myData.Add(data);
                    }
                    break;
                case "Admin":
                case "DemoAdmin":
                    foreach (var type in db.TicketTypes.ToList())
                    {
                        data = new MorrisBarData();
                        data.label = type.Name;
                        data.value = db.Tickets.Where(t => t.TicketType.Name == type.Name).Count();
                        myData.Add(data);
                    }
                    break;
            }
            return Json(myData);
        }
        //public ActionResult Dashboard()
        //{
        //    var id = User.Identity.GetUserId();
        //    var user = db.Users.Find(id);
        //    return View(new DashboardVM()
        //    {
        //        FullName = user.FullName,
        //        AssignedProjects = user.Projects.ToList(),
        //        Tickets = db.Tickets.ToList(),
        //        SubmittedTicketCount = db.Tickets.Where(t => t.SubmitterId == id).ToList().Count(),
        //        AssignedTicketCount = db.Tickets.Where(t => t.DeveloperId == id).ToList().Count(),
        //        AssignedProjectCount = projectHelper.ListUserProjects(id).Count(),
        //        Status = db.TicketStatuses.ToList(),
        //        Priority = db.TicketPriorities.ToList(),
        //        Type = db.TicketTypes.ToList(),
        //        Projects = db.Projects.ToList(),
        //        HighPriorityTicketCount = ticketHelper.ListMyTicketsByPriority("High").Count(),
        //        UnassignedTicketCount = ticketHelper.ListUnassignedTickets().Count(),
        //        RecentResolvedCount = ticketHelper.ListRecentResolvedTickets().Count()
        //    });
        //}

    }
}