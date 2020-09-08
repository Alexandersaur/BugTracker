using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BugTracker.Helpers
{
    public class ProjectHelper
    {
        //Needed functions: add or remove one or more users from a project, list users on or off a project
        //optional: list users on / not on a project that occupy a specific role

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private UserHelper userHelper = new UserHelper();

        public bool IsUserOnProject(string userId, int projectId)
        {
            Project project = db.Projects.Find(projectId);
            return project.Users.Any(u => u.Id == userId);
        }
        public int NumberOfProjects()
        {
            var currentNumberOfProjects = db.Projects.Count();
            return currentNumberOfProjects;

        }
        public bool CanCreateProject()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                case "ProjectManager":
                    return true;
                default:
                    return false;
            }
        }
        public bool CanEditProject()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                case "ProjectManager":
                    return true;
                default:
                    return false;
            }
        }
        public bool CanViewAllProjects()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                case "ProjectManager":
                    return true;
                default:
                    return false;
            }
        }
        public void AddUserToProject(string userId, int projectId) 
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Project project = db.Projects.Find(projectId);
                var user = db.Users.Find(userId);
                project.Users.Add(user);
                db.SaveChanges();
            }
        }
        public bool RemoveUserFromProject(string userId, int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var user = db.Users.Find(userId);
            var result = project.Users.Remove(user);
            db.SaveChanges();
            return result;
        }
        public List<ApplicationUser> ListUsersOnProject(int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var resultList = new List<ApplicationUser>();
            resultList.AddRange(project.Users);
            return resultList;
        }
        public List<Project> ListUserProjects(string userId)
        {
            var user = db.Users.Find(userId);
            var resultList = new List<Project>();
            resultList.AddRange(user.Projects);
            return resultList;
        }
        public List<ApplicationUser> ListUsersNotOnProject(int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var resultList = new List<ApplicationUser>();
            foreach(var user in db.Users.ToList())
            {
                if(!IsUserOnProject(user.Id, projectId))
                {
                    resultList.Add(user);
                }
            }
            return resultList;
        }
        public List<ApplicationUser> ListUsersOnProjectInRole(int projectId, string roleName)
        {
            var userList = ListUsersOnProject(projectId);
            var resultList = new List<ApplicationUser>();
            foreach (var user in userList)
            {
                if (roleHelper.IsUserInRole(user.Id, roleName))
                {
                    resultList.Add(user);
                }
            }
            return resultList;
        }
    }
}