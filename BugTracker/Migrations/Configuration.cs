namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        public string Email { get; private set; }
        public string UserName { get; private set; }

        #region Role and User Code 
        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            //I want to go out to the DB and see if a particular role is already present

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole() { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole() { Name = "ProjectManager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole() { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole() { Name = "Submitter" });
            }
            //writes some code that creates a user

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //now i need to go out and look for the presence of a user with a specific email
            // if and only if it is not found will i create a user with that email

            if (!context.Users.Any(u => u.Email == "jeremy.a.steward@gmail.com"))
            {

                userManager.Create(new ApplicationUser()
                {
                    Email = "jeremy.a.steward@gmail.com",
                    UserName = "jeremy.a.steward@gmail.com",
                    FirstName = "Jeremy",
                    LastName = "Steward",
                }, "Abc&123!");

                userManager.Create(new ApplicationUser()
                {
                    Email = "jasontwichell@coderfoundry.com",
                    UserName = "jasontwichell@coderfoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                }, "Abc&123!");

                userManager.Create(new ApplicationUser()
                {
                    Email = "andrewrussell@coderfoundry.com",
                    UserName = "andrewrussell@coderfoundry.com",
                    FirstName = "Andrew",
                    LastName = "Russell",
                }, "Abc&123!");

                userManager.Create(new ApplicationUser()
                {
                    Email = "alexandersaur@icloud.com",
                    UserName = "alexandersaur@icloud.com",
                    FirstName = "Alex",
                    LastName = "Ander",
                }, "Abc&123!");

                //Step 1: Grab the id that was just created by adding the above user
                var userId = userManager.FindByEmail("jeremy.a.steward@gmail.com").Id;
                //now I want to assign the user to a specific role
                userManager.AddToRole(userId, "Admin");

                var userId2 = userManager.FindByEmail("jasontwichell@coderfoundry.com").Id;
                userManager.AddToRole(userId2, "ProjectManager");

                var userId3 = userManager.FindByEmail("andrewrussell@coderfoundry.com").Id;
                userManager.AddToRole(userId3, "Developer");

                var userId4 = userManager.FindByEmail("alexandersaur@icloud.com").Id;
                userManager.AddToRole(userId4, "Submitter");
            }
            #endregion

            context.SaveChanges();

            #region TicketType Seed
            context.TicketTypes.AddOrUpdate(
                tt => tt.Name,
                new TicketType() { Name = "Software" },
                new TicketType() { Name = "Hardware" },
                new TicketType() { Name = "UI" },
                new TicketType() { Name = "Defect" },
                new TicketType() { Name = "Feature Request" },
                new TicketType() { Name = "Other" }
                );
            #endregion
            #region TicketPriority Seed
            context.TicketPriorities.AddOrUpdate(
                tp => tp.Name,
                new TicketPriority() { Name = "Low" },
                new TicketPriority() { Name = "Medium" },
                new TicketPriority() { Name = "High" },
                new TicketPriority() { Name = "On Hold" }
                );
            #endregion
            #region TicketStatus Seed
            context.TicketStatuses.AddOrUpdate(
                ts => ts.Name,
                new TicketStatus() { Name = "Open" },
                new TicketStatus() { Name = "Assigned" },
                new TicketStatus() { Name = "Resolved" },
                new TicketStatus() { Name = "Reopened" },
                new TicketStatus() { Name = "Archived" }
                );
            #endregion
            #region Project Seed
            context.Projects.AddOrUpdate(
                p => p.Name,
                new Project() { Name = "Seed 1", Created = DateTime.Now.AddDays(-60), IsArchived = true },
                new Project() { Name = "Seed 2", Created = DateTime.Now.AddDays(-45), IsArchived = false },
                new Project() { Name = "Seed 3", Created = DateTime.Now.AddDays(-30), IsArchived = false },
                new Project() { Name = "Seed 4", Created = DateTime.Now.AddDays(-15), IsArchived = false },
                new Project() { Name = "Seed 5", Created = DateTime.Now.AddDays(-7), IsArchived = false }
                );
            #endregion

            context.SaveChanges();

            #region Seed Priority, Status, and Type
            if (!context.TicketTypes.Any())
            {
                var types = new List<TicketType>()
                {
                    new TicketType() { Name = "Defect/Bug", Description = "Reference a defect in a production release of a project" },
                    new TicketType() { Name = "Documentation Request", Description = "Reference a client request for documentation in a production release of a project" },
                    new TicketType() { Name = "Training request", Description = "Reference a client request for training in a production release of a project" }
                };
                context.TicketTypes.AddRange(types);
            }
            if (!context.TicketPriorities.Any())
            {
                var priorities = new List<TicketPriority>()
            {
                new TicketPriority() { Name = "Immediate" },
                new TicketPriority() { Name = "High" },
                new TicketPriority() { Name = "Medium" },
                new TicketPriority() { Name = "Low" },
                new TicketPriority() { Name = "None" }
            };
                context.TicketPriorities.AddRange(priorities);
            }
            if (!context.TicketStatuses.Any())
            {
                var statuses = new List<TicketStatus>()
            {
                new TicketStatus() { Name = "New" },
                new TicketStatus() { Name = "Unassigned" },
                new TicketStatus() { Name = "Assigned" },
                new TicketStatus() { Name = "In Progress" },
                new TicketStatus() { Name = "Done" },
                new TicketStatus() { Name = "Archived" }
            };
                context.TicketStatuses.AddRange(statuses);
            }
            #endregion
        }
    }
}

