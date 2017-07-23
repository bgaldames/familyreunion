namespace FamilyReunion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Duties",
                c => new
                    {
                        DutyId = c.Int(nullable: false, identity: true),
                        DutyTypeId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.DutyId)
                .ForeignKey("dbo.DutyTypes", t => t.DutyTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId)
                .Index(t => t.DutyTypeId)
                .Index(t => t.Team_TeamId);
            
            CreateTable(
                "dbo.DutyTypes",
                c => new
                    {
                        DutyTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DutyTypeId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        ReunionId = c.Int(nullable: false),
                        TeamLead = c.Int(),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.Reunions", t => t.ReunionId, cascadeDelete: true)
                .Index(t => t.ReunionId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        MemberTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        CellPhone = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.MemberTypes", t => t.MemberTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId)
                .Index(t => t.MemberTypeId)
                .Index(t => t.Team_TeamId);
            
            CreateTable(
                "dbo.FamilyMembers",
                c => new
                    {
                        FamilyMemberId = c.Int(nullable: false, identity: true),
                        FamilyId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FamilyMemberId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.MemberTypes",
                c => new
                    {
                        MemberTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MemberTypeId);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        FamilyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.FamilyId);
            
            CreateTable(
                "dbo.Reunions",
                c => new
                    {
                        ReunionId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ReunionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Hometown = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Teams", "ReunionId", "dbo.Reunions");
            DropForeignKey("dbo.Members", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Members", "MemberTypeId", "dbo.MemberTypes");
            DropForeignKey("dbo.FamilyMembers", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Duties", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Duties", "DutyTypeId", "dbo.DutyTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FamilyMembers", new[] { "MemberId" });
            DropIndex("dbo.Members", new[] { "Team_TeamId" });
            DropIndex("dbo.Members", new[] { "MemberTypeId" });
            DropIndex("dbo.Teams", new[] { "ReunionId" });
            DropIndex("dbo.Duties", new[] { "Team_TeamId" });
            DropIndex("dbo.Duties", new[] { "DutyTypeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reunions");
            DropTable("dbo.Families");
            DropTable("dbo.MemberTypes");
            DropTable("dbo.FamilyMembers");
            DropTable("dbo.Members");
            DropTable("dbo.Teams");
            DropTable("dbo.DutyTypes");
            DropTable("dbo.Duties");
        }
    }
}
