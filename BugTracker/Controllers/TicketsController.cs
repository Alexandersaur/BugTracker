﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Helpers;
using BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    //[Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper projectHelper = new ProjectHelper();
        private UserRoleHelper userRoleHelper = new UserRoleHelper();

        // GET: Tickets
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var myRole = userRoleHelper.ListUserRoles(userId).FirstOrDefault();
            List<Ticket> model = new List<Ticket>();
            switch(myRole)
            {
                case "Admin":
                    model = db.Tickets.ToList();
                    break;
                case "Project Manager":
                case "Developer":
                    model = projectHelper.ListUserProjects(userId).SelectMany(p => p.Tickets).ToList();
                    break;
                case "Submitter":
                    model = db.Tickets.Where(t => t.SubmitterId == userId).ToList();
                    break;
                default: 
                    return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        //[Authorize(Roles = "Submitter")]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            if(userId == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId), "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Create([Bind(Include = "Id,ProjectId,TicketPriorityId,TicketTypeId,Issue,IssueDescription,IsResolved,IsArchived")] Ticket ticket)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                //Add back in: Created, SubmitterId
                //Set: DeveloperId to null, IsArchived and IsResolved will be false
                ticket.TicketStatusId = db.TicketStatuses.Where(ts => ts.Name == "Open").FirstOrDefault().Id;
                ticket.Created = DateTime.Now;
                ticket.SubmitterId = userId;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = ticket.ProjectId });
            }

            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId), "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketPriorityId,TicketStatusId,TicketTypeId,SubmitterId,DeveloperId,Issue,IssueDescription,Created,Updated,IsResolved,IsArchived")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
