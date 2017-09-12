namespace FamilyReunion.Migrations
{
    using FamilyReunion.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FamilyReunion.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "FamilyReunion.Models.ApplicationDbContext";
        }

        protected override void Seed(FamilyReunion.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.MemberTypes.AddOrUpdate(
             p => p.Name,
             new MemberType { Name = "Me" },
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
