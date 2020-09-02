namespace BugTracker.Migrations
{
    using BugTracker.Helpers;
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        private ProjectHelper projectHelper = new ProjectHelper();
        private UserRoleHelper userRoleHelper = new UserRoleHelper();
        Random random = new Random();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        public string Email { get; private set; }
        public string UserName { get; private set; }

        #region Role Creation
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
            if (!context.Roles.Any(r => r.Name == "New USer"))
            {
                roleManager.Create(new IdentityRole() { Name = "New User" });
            }
            //writes some code that creates a user

            #endregion
            #region User Creation

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];

            List<string> firstNames = new List<string>() { "John", "Jacob", "David", "Jordan", "Robin", "Spencer", "Steven", "Greg", "Andrew", "Rachel", "Susannah" };
            List<string> lastNames = new List<string>() { "Smith", "Jones", "Robinson", "Knight", "Jackson", "Esposito", "McAdams", "Heymann", "Russell", "Silverman", "O'Donnell" };

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
                    AvatarPath = "/Images/default_avatar.png"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "jasontwichell@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "jasontwichell@coderfoundry.com",
                    UserName = "jasontwichell@coderfoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    AvatarPath = "/Images/default_avatar.png"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "andrewrussell@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "andrewrussell@coderfoundry.com",
                    UserName = "andrewrussell@coderfoundry.com",
                    FirstName = "Andrew",
                    LastName = "Russell",
                    AvatarPath = "/Images/default_avatar.png"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "alexandersaur@icloud.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "alexandersaur@icloud.com",
                    UserName = "alexandersaur@icloud.com",
                    FirstName = "Alex",
                    LastName = "Ander",
                    AvatarPath = "/Images/default_avatar.png"
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "DemoAdminJS@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoAdminJS@mailinator.com",
                    UserName = "DemoAdminJS@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoProjectManagerJS@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoProjectManagerJS@mailinator.com",
                    UserName = "DemoProjectManagerJS@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoProjectManagerJSTwo@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoProjectManagerJSTwo@mailinator.com",
                    UserName = "DemoProjectManagerJSTwo@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloperJS@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoDeveloperJS@mailinator.com",
                    UserName = "DemoDeveloperJS@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloperJSTwo@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoDeveloperJSTwo@mailinator.com",
                    UserName = "DemoDeveloperJSTwo@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloperJSThree@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoDeveloperJSThree@mailinator.com",
                    UserName = "DemoDeveloperJSThree@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitterJS@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoSubmitterJS@mailinator.com",
                    UserName = "DemoSubmitterJS@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitterJSTwo@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoSubmitterJSTwo@mailinator.com",
                    UserName = "DemoSubmitterJSTwo@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitterJSThree@mailinator.com"))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = "DemoSubmitterJSThree@mailinator.com",
                    UserName = "DemoSubmitterJSThree@mailinator.com",
                    FirstName = firstNames[random.Next(firstNames.Count)],
                    LastName = lastNames[random.Next(lastNames.Count)],
                    AvatarPath = "/Images/default_avatar.png"
                }, "DemoPassword");
            }

            #endregion
            #region Role Assignment

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

            var DemoAdminJS = userManager.FindByEmail("DemoAdminJS@mailinator.com").Id;
            userManager.AddToRole(DemoAdminJS, "Admin");

            var DemoProjectManagerJS = userManager.FindByEmail("DemoProjectManagerJS@mailinator.com").Id;
            userManager.AddToRole(DemoProjectManagerJS, "ProjectManager");

            var DemoProjectManagerJSTwo = userManager.FindByEmail("DemoProjectManagerJSTwo@mailinator.com").Id;
            userManager.AddToRole(DemoProjectManagerJSTwo, "ProjectManager");

            var DemoDeveloperJS = userManager.FindByEmail("DemoDeveloperJS@mailinator.com").Id;
            userManager.AddToRole(DemoDeveloperJS, "Developer");

            var DemoDeveloperJSTwo = userManager.FindByEmail("DemoDeveloperJSTwo@mailinator.com").Id;
            userManager.AddToRole(DemoDeveloperJSTwo, "Developer");

            var DemoDeveloperJSThree = userManager.FindByEmail("DemoDeveloperJSThree@mailinator.com").Id;
            userManager.AddToRole(DemoDeveloperJSThree, "Developer");

            var DemoSubmitterJS = userManager.FindByEmail("DemoSubmitterJS@mailinator.com").Id;
            userManager.AddToRole(DemoSubmitterJS, "Submitter");

            var DemoSubmitterJSTwo = userManager.FindByEmail("DemoSubmitterJSTwo@mailinator.com").Id;
            userManager.AddToRole(DemoSubmitterJSTwo, "Submitter");

            var DemoSubmitterJSThree = userManager.FindByEmail("DemoSubmitterJSThree@mailinator.com").Id;
            userManager.AddToRole(DemoSubmitterJSThree, "Submitter");

            #endregion

            context.SaveChanges();

            #region TicketType Seed
            context.TicketTypes.AddOrUpdate(
                tt => tt.Name,
                new TicketType() { Name = "Software" },
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
                new Project() { Name = "Project Seed 1", Created = DateTime.Now.AddDays(-60), IsArchived = true },
                new Project() { Name = "Project Seed 2", Created = DateTime.Now.AddDays(-45), IsArchived = false },
                new Project() { Name = "Project Seed 3", Created = DateTime.Now.AddDays(-30), IsArchived = false },
                new Project() { Name = "Project Seed 4", Created = DateTime.Now.AddDays(-15), IsArchived = false },
                new Project() { Name = "Project Seed 5", Created = DateTime.Now.AddDays(-7), IsArchived = false }
                );
            #endregion

            context.SaveChanges();

            #region Seed Priority, Status, and Type (alternate coding style)
            //if (!context.TicketTypes.Any())
            //{
            //    var types = new List<TicketType>()
            //    {
            //        new TicketType() { Name = "Defect/Bug", Description = "Reference a defect in a production release of a project" },
            //        new TicketType() { Name = "Documentation Request", Description = "Reference a client request for documentation in a production release of a project" },
            //        new TicketType() { Name = "Training request", Description = "Reference a client request for training in a production release of a project" }
            //    };
            //    context.TicketTypes.AddRange(types);
            //}
            //if (!context.TicketPriorities.Any())
            //{
            //    var priorities = new List<TicketPriority>()
            //{
            //    new TicketPriority() { Name = "Immediate" },
            //    new TicketPriority() { Name = "High" },
            //    new TicketPriority() { Name = "Medium" },
            //    new TicketPriority() { Name = "Low" },
            //    new TicketPriority() { Name = "None" }
            //};
            //    context.TicketPriorities.AddRange(priorities);
            //}
            //if (!context.TicketStatuses.Any())
            //{
            //    var statuses = new List<TicketStatus>()
            //{
            //    new TicketStatus() { Name = "New" },
            //    new TicketStatus() { Name = "Unassigned" },
            //    new TicketStatus() { Name = "Assigned" },
            //    new TicketStatus() { Name = "In Progress" },
            //    new TicketStatus() { Name = "Done" },
            //    new TicketStatus() { Name = "Archived" }
            //};
            //    context.TicketStatuses.AddRange(statuses);
            //}
            #endregion

            //context.SaveChanges();

            var userList = context.Users.ToList();
            var projectList = context.Projects.ToList();
            foreach (var project in projectList)
            {
                //int count = 1;
                foreach (var user in userList)
                {
                    //if(count % 2 != 0)
                    //{
                    projectHelper.AddUserToProject(user.Id, project.Id);
                    //}
                }
                //count++
            }

            #region Ticket Seed

            List<Ticket> ticketList = new List<Ticket>();
            List<ApplicationUser> projectManagers = new List<ApplicationUser>();
            List<ApplicationUser> developers = new List<ApplicationUser>();
            List<ApplicationUser> submitters = new List<ApplicationUser>();
            projectManagers.AddRange(userRoleHelper.UsersInRole("ProjectManager"));
            developers.AddRange(userRoleHelper.UsersInRole("Developer"));
            submitters.AddRange(userRoleHelper.UsersInRole("Submitter"));

            #region Assigning Users to Projects by Role
            foreach (var project in context.Projects)
            {
                //Admin Assignment
                foreach (var user in userRoleHelper.UsersInRole("Admin"))
                {
                    projectHelper.AddUserToProject(user.Id, project.Id);
                }
                //Project Manager Assignment
                projectHelper.AddUserToProject(projectManagers[random.Next(projectManagers.Count)].Id, project.Id);
                //Developer Assignment
                var firstDev = developers[random.Next(developers.Count)].Id;
                var secondDev = developers[random.Next(developers.Count)].Id;
                while(firstDev == secondDev)
                {
                    secondDev = developers[random.Next(developers.Count)].Id;
                }
                projectHelper.AddUserToProject(firstDev, project.Id);
                projectHelper.AddUserToProject(secondDev, project.Id);
                //Subitter Assignment
                var firstSub = submitters[random.Next(submitters.Count)].Id;
                var secondSub = submitters[random.Next(submitters.Count)].Id;
                while (firstSub == secondSub)
                {
                    secondSub = submitters[random.Next(submitters.Count)].Id;
                }
                projectHelper.AddUserToProject(firstSub, project.Id);
                projectHelper.AddUserToProject(secondSub, project.Id);
            }
            #endregion

            foreach(var project in context.Projects.ToList())
            {
                projectManagers = projectHelper.ListUsersOnProjectInRole(project.Id, "ProjectManager");
                developers = projectHelper.ListUsersOnProjectInRole(project.Id, "Developer");
                submitters = projectHelper.ListUsersOnProjectInRole(project.Id, "Submitter");
                foreach(var type in context.TicketTypes.ToList())
                {
                    foreach(var status in context.TicketStatuses.ToList())
                    {
                        foreach(var priority in context.TicketPriorities.ToList())
                        {
                            var developerId = developers[random.Next(developers.Count)].Id;
                            if(status.Name == "Open")
                            {
                                developerId = null;
                            }
                            var resolved = false;
                            var archived = false;
                            if(status.Name == "Resolved")
                            {
                                resolved = true;
                            }
                            if(status.Name == "Archived" || project.IsArchived)
                            {
                                archived = true;
                            }
                            var newTicket = new Ticket()
                            {
                                ProjectId = project.Id,
                                TicketPriorityId = priority.Id,
                                TicketTypeId = type.Id,
                                TicketStatusId = status.Id,
                                SubmitterId = submitters[random.Next(submitters.Count)].Id,
                                DeveloperId = developerId,
                                Created = DateTime.Now,
                                Issue = $"This is a seeded ticket of type {type.Name} with a status of {status.Name} and {priority.Name} priority.",
                                IssueDescription = $"This is a description of a ticket of type {type.Name} on {project.Name}.",
                                IsResolved = resolved,
                                IsArchived = archived
                            };
                            ticketList.Add(newTicket);
                        }
                    }
                }
            }
            context.Tickets.AddRange(ticketList);
            context.SaveChanges();
            #endregion
        }
    }
}

