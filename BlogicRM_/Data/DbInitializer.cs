using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogicRM_.Models;

namespace BlogicRM_.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogicRM context)
        {
            //context.Database.EnsureCreated();

            // Look for any Clients.
            if (context.Contract.Any())
            {
                return;   // DB has been seeded
            }

            var Clients = new Client[]
            {
                new Client { Name = "Carson",   Surname = "Alexander",
                     BirthNumber = "101010/9090", Age=10, Phone = "123123123", Email = "x@x.com" },
                new Client { Name = "Test",   Surname = "User",
                     BirthNumber = "101011/9090", Age=30, Phone = "123125123", Email = "xx@xx.com" },
            };

            foreach (Client s in Clients)
            {
                context.Client.Add(s);
            }
            context.SaveChanges();

            var Advisors = new Advisor[]
            {
                
                new Advisor { Name = "ADVISOR",   Surname = "1",
                     BirthNumber = "101010/9090", Age=10, Phone = "123123123", Email = "x@x.com" },
                new Advisor { Name = "ADVISOR",   Surname = "2",
                     BirthNumber = "101011/9090", Age=30, Phone = "123125123", Email = "xx@xx.com" },
            };

            foreach (Advisor s in Advisors)
            {
                context.Advisor.Add(s);
            }
            context.SaveChanges();

            var Institutions = new Institution[]
{
                new Institution { Name = "ČSOB" },
                new Institution { Name = "AXA" }
};

            foreach (Institution s in Institutions)
            {
                context.Institution.Add(s);
            }
            context.SaveChanges();

            var Contracts = new Contract[]
{
                new Contract {
                    AdministratorID = Advisors.Single( i => i.Surname == "1").AdvisorID,
                    EvidenceNumber = "100", InstitutionID = Institutions.Single( i => i.Name == "ČSOB").InstitutionID, ClientID = Clients.Single( i => i.Name == "Test").ClientID,
                    ConclusionDate = DateTime.Parse("2020-09-01"), EndDate = DateTime.Parse("2022-09-01"), ValidityDate = DateTime.Parse("2020-10-01") },

                new Contract {
                    AdministratorID = Advisors.Single( i => i.Surname == "2").AdvisorID,
                    EvidenceNumber = "101", InstitutionID = Institutions.Single( i => i.Name == "AXA").InstitutionID, ClientID = Clients.Single( i => i.Name == "Carson").ClientID,
                    ConclusionDate = DateTime.Parse("2019-09-01"), EndDate = DateTime.Parse("2019-12-01"), ValidityDate = DateTime.Parse("2019-09-07") },

};

            foreach (Contract co in Contracts)
            {
                context.Contract.Add(co);
            }
            context.SaveChanges();


            var ContractAdvisors = new ContractAdvisor[]
{
                new ContractAdvisor {
                    ContractID = Contracts.Single(c => c.EvidenceNumber == "100" ).ContractID,
                    AdvisorID = Advisors.Single(i => i.Surname == "1").AdvisorID
                    },
                new ContractAdvisor {
                    ContractID = Contracts.Single(c => c.EvidenceNumber == "100" ).ContractID,
                    AdvisorID = Advisors.Single(i => i.Surname == "2").AdvisorID
                    },
                new ContractAdvisor {
                    ContractID = Contracts.Single(c => c.EvidenceNumber == "101" ).ContractID,
                    AdvisorID = Advisors.Single(i => i.Surname == "2").AdvisorID
                    },
};

            foreach (ContractAdvisor ca in ContractAdvisors)
            {
                context.contractAdvisor.Add(ca);
            }
            context.SaveChanges();

        }
    }
}