using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class TicketHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper userRoleHelper = new UserRoleHelper();
        private HistoryHelper historyHelper = new HistoryHelper();

        public List<Ticket> GetMyTickets()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var tickets = new List<Ticket>();
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                    tickets.AddRange(db.Tickets);
                    break;
                case "ProjectManager":
                    tickets.AddRange(db.Users.Find(userId).Projects.SelectMany(p => p.Tickets));
                    break;
                case "Developer":
                    tickets.AddRange(db.Tickets.Where(t => t.DeveloperId == userId));
                    break;
                case "Submitter":
                    tickets.AddRange(db.Tickets.Where(t => t.SubmitterId == userId));
                    break;
                default:
                    break;
            }
            return tickets;
        }
        public List<Ticket> ListMyTicketsByPriority(string priority)
        {
            var myTickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                case "DemoAdmin":
                    myTickets.AddRange(db.Tickets.Where(t => t.TicketPriority.Name == priority));
                    break;
                case "Project Manager":
                case "DemoProjectManager":
                    myTickets.AddRange(user.Projects.Where(p => p.IsArchived == false).SelectMany(p => p.Tickets.Where(t => t.TicketPriority.Name == priority)));
                    break;
                case "Developer":
                case "DemoDeveloper":
                    myTickets.AddRange(db.Tickets.Where(t => t.IsArchived == false).Where(t => t.TicketPriority.Name == priority).Where(t => t.DeveloperId == userId));
                    break;
                case "Submitter":
                case "DemoSubmitter":
                    myTickets.AddRange(db.Tickets.Where(t => t.IsArchived == false).Where(t => t.TicketPriority.Name == priority).Where(t => t.SubmitterId == userId));
                    break;
            }
            return myTickets;
        }

        public List<Ticket> ListUnassignedTickets(string unassigned)
        {
            var myTickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                case "DemoAdmin":
                    myTickets.AddRange(db.Tickets.Where(t => t.TicketPriority.Name == unassigned));
                    break;
                case "Project Manager":
                case "DemoProjectManager":
                    myTickets.AddRange(user.Projects.Where(p => p.IsArchived == false).SelectMany(p => p.Tickets.Where(t => t.TicketPriority.Name == unassigned)));
                    break;
                case "Developer":
                case "DemoDeveloper":
                    myTickets.AddRange(db.Tickets.Where(t => t.IsArchived == false).Where(t => t.TicketPriority.Name == unassigned).Where(t => t.DeveloperId == userId));
                    break;
                case "Submitter":
                case "DemoSubmitter":
                    myTickets.AddRange(db.Tickets.Where(t => t.IsArchived == false).Where(t => t.TicketPriority.Name == unassigned).Where(t => t.SubmitterId == userId));
                    break;
            }
            return myTickets;
        }

        public bool CanMakeComment(int ticketId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                    return true;
                case "Project Manager":
                    var user = db.Users.Find(userId);
                    //var projects = user.Projects;
                    //var tickets = projects.SelectMany(p => p.Tickets);
                    //var bool1 = tickets.Any(t => t.Id == ticketId);
                    return (user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId));
                case "Developer":
                case "Submitter":
                    var ticket = db.Tickets.Find(ticketId);
                    if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        public int TotalNumberOfTickets()
        {
            var currentNumberOfTickets = db.Tickets.Count();
            return currentNumberOfTickets;
        }

        public bool CanEditTicket(int ticketId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                    return true;
                case "Project Manager":
                    var user = db.Users.Find(userId);
                    return (user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId));
                case "Developer":
                case "Submitter":
                    var ticket = db.Tickets.Find(ticketId);
                    if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        public bool CanCreateTicket()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                case "Submitter":
                    return true;
                default:
                    return false;
            }
        }
        public void ManageTicketNotifications(Ticket oldTicket, Ticket newTicket)
        {
            //Scenario 1: a new assignment - oldTicket.DeveloperId = null, and new Ticket.DeveloperId is not null
            if (oldTicket.DeveloperId != newTicket.DeveloperId && newTicket.DeveloperId != null)
            {
                //I have determined that this change needs a notification and I need to create a new TicketNotification record
                var newNotification = new TicketNotification()
                {
                    TicketId = newTicket.Id,
                    UserId = newTicket.DeveloperId,
                    Created = DateTime.Now,
                    Subject = $"You have been assigned to Ticket Id: {newTicket.Id}",
                    Message = $"Heads up {newTicket.Developer.FullName}, you have been assigned to Ticket Id {newTicket.Id} with the following issue: '{newTicket.Issue}', on Project '{newTicket.Project.Name}'"
                };
                db.TicketNotifications.Add(newNotification);
                db.SaveChanges();
            }
            //Scenario 2: an unassignment - oldTicket.DeveloperId was not null, and new Ticket.DeveloperId is null
            if (oldTicket.DeveloperId != newTicket.DeveloperId && oldTicket.DeveloperId != null)
            {
                var oldNotification = new TicketNotification()
                {
                    TicketId = oldTicket.Id,
                    UserId = oldTicket.DeveloperId,
                    Created = DateTime.Now,
                    Subject = $"You have been removed from Ticket Id: {oldTicket.Id}",
                    Message = $"Heads up {oldTicket.Developer.FullName}, you have been removed from Ticket Id {oldTicket.Id} with the following issue: '{oldTicket.Issue}', on Project '{oldTicket.Project.Name}'"
                };
                db.TicketNotifications.Add(oldNotification);
                db.SaveChanges();
            }
            //Scenario 3: a reassignment - neither old nor new ticket.DeveloperId is null, and they dont match (this could create two notifactions, one for the new user and one for the old user)
            //if (oldTicket.DeveloperId != newTicket.DeveloperId && oldTicket.DeveloperId != null && newTicket.DeveloperId != null)
            //{
                //var oldNotification = new TicketNotification()
                //{
                //    TicketId = oldTicket.Id,
                //    UserId = oldTicket.DeveloperId,
                //    Created = DateTime.Now,
                //    Subject = $"You have been removed from Ticket Id: {oldTicket.Id}",
                //    Message = $"Heads up {oldTicket.Developer.FullName}, you have been removed from Ticket Id {oldTicket.Id} with the following issue: '{oldTicket.Issue}', on Project '{oldTicket.Project.Name}'"
                //};
                //db.TicketNotifications.Add(oldNotification);
                //db.SaveChanges();
            //}
        }
        public void EditedTicket(Ticket oldTicket, Ticket newTicket)
        {
            historyHelper.ManageHistories(oldTicket, newTicket);
            ManageTicketNotifications(oldTicket, newTicket);
        }
    }
}