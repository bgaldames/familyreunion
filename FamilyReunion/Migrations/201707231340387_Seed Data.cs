namespace FamilyReunion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "IsEligibleForWork", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "IsEligibleForWork");
        }
    }
}
