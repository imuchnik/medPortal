using System.Collections.Generic;
using System.ServiceModel;
using MedicationTherapyManagement.Models;

namespace MedicationTherapyManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration :
        DbMigrationsConfiguration<MedicationTherapyManagement.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MedicationTherapyManagement.Models.ApplicationDbContext";
        }

        protected override void Seed(MedicationTherapyManagement.Models.ApplicationDbContext context)
        {
            {
                this.AddUserAndRoles(context);
            }
        }


        private bool AddUserAndRoles(ApplicationDbContext context)
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;

            success = idManager.CreateRole("Provider");
            if (!success == true) return success;

            success = idManager.CreateRole("Coordinator");
            if (!success) return success;


            var newUser = new ApplicationUser()
            {
                UserName = "imuchnik",
                FirstName = "Irina",
                LastName = "Muchnik"
                OrganizationId = 1
            };
           

            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            AddClientsAndOrganizations(context);
            success = idManager.CreateUser(newUser, "password1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;

          

            return success;
        }

        private static void AddClientsAndOrganizations(ApplicationDbContext context)
        { 

     
           
          var organization = new Organization()
            {
                OrganizationId = 1,
                OrganizationName = "Ventegra",
                Clients = new List<Client>()
            };
            context.Organizations.Add(organization);
            context.SaveChanges();

          
            var clients = new List<Client>()
            {
                new Client()
                {
                    ClientId = 1,
                    ClientName = "UTN"
,                    OrganizationId = 1
                },
                new Client() {ClientId = 2, ClientName = "MINES", OrganizationId = 1},
                new Client() {ClientId = 3, ClientName = "SHIP", OrganizationId = 1}
            };
            clients.ForEach(c => context.Clients.AddOrUpdate(cl => cl.ClientId, c));
            context.SaveChanges();

        }
    }
}