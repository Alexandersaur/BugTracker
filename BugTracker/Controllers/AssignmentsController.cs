using BugTracker.Helpers;
using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        // GET: Assignments
        public ActionResult ManageRoles()
        {
            //use my ViewBag to hold a MultiSelectList of User in the system
            //new MultiSelectList(the data, the data's primary key / "Id" which is transmitted, what property are we showing the user)
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");
            //use ViewBag to hold a SelectList of Roles
            //.Where(r => r.Name !="Admin")//
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name");
            return View(db.Users.ToList());
        }
        public ActionResult ManageUserRoles()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string roleName)
        {
            //Step 1 - if anyone was selected, remove them from all their roles
            if (userIds == null)
                return RedirectToAction("RolesIndex");
            foreach(var userId in userIds)
            {
                //Determine if this user occupies a role
                foreach(var role in roleHelper.ListUserRoles(userId).ToList())
                {
                    roleHelper.RemoveUserFromRole(userId, role);
                }
                //Step 2 - if a role was chosen, add each person to that role
                if (!string.IsNullOrEmpty(roleName))
                {
                    roleHelper.AddUserToRole(userId, roleName);
                }
            }
            //return RedirectToAction("ManageRoles"); 
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name");
            return View(db.Users.ToList());
        }
    }
}