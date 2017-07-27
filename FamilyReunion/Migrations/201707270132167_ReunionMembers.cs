namespace FamilyReunion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReunionMembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReunionMembers",
                c => new
                    {
                        ReunionMemberId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        IsAttending = c.Boolean(nullable: false),
                        Reason = c.String(),
                        Reunion_ReunionId = c.Int(),
                    })
                .PrimaryKey(t => t.ReunionMemberId)
                .ForeignKey("dbo.Reunions", t => t.Reunion_ReunionId)
                .Index(t => t.Reunion_ReunionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReunionMembers", "Reunion_ReunionId", "dbo.Reunions");
            DropIndex("dbo.ReunionMembers", new[] { "Reunion_ReunionId" });
            DropTable("dbo.ReunionMembers");
        }
    }
}
