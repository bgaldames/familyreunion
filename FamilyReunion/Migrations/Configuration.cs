namespace FamilyReunion.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FamilyReunion.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FamilyReunion.Models.ApplicationDbContext";
        }

        protected override void Seed(FamilyReunion.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.MemberTypes.AddOrUpdate(
              p => p.Name,
              new MemberType { Name = "Mom" },
              new MemberType { Name = "Dad" },
              new MemberType { Name = "Son" },
              new MemberType { Name = "Daughter" },
              new MemberType { Name = "Grand Ma" }
            );
            context.DutyTypes.AddOrUpdate(
              p => p.Name,
              new DutyType { Name = "Cook" },
              new DutyType { Name = "Clean" }
            );
        }
    }
}
