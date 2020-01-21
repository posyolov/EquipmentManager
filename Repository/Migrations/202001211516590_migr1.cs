namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Position_Name = c.String(maxLength: 128),
                        JournalEntryCategory_Id = c.Int(),
                        PositionStatusBitInfo_BitNumber = c.Int(),
                        IsIncoming = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JournalEntryCategories", t => t.JournalEntryCategory_Id)
                .ForeignKey("dbo.Positions", t => t.Position_Name)
                .ForeignKey("dbo.PositionStatusBitsInfo", t => t.PositionStatusBitInfo_BitNumber)
                .Index(t => t.Position_Name)
                .Index(t => t.JournalEntryCategory_Id)
                .Index(t => t.PositionStatusBitInfo_BitNumber);
            
            CreateTable(
                "dbo.JournalEntryCategories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        ParentName = c.String(),
                        Title = c.String(),
                        Status = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.PositionStatusBitsInfo",
                c => new
                    {
                        BitNumber = c.Int(nullable: false),
                        Enable = c.Boolean(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.BitNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journal", "PositionStatusBitInfo_BitNumber", "dbo.PositionStatusBitsInfo");
            DropForeignKey("dbo.Journal", "Position_Name", "dbo.Positions");
            DropForeignKey("dbo.Journal", "JournalEntryCategory_Id", "dbo.JournalEntryCategories");
            DropIndex("dbo.Journal", new[] { "PositionStatusBitInfo_BitNumber" });
            DropIndex("dbo.Journal", new[] { "JournalEntryCategory_Id" });
            DropIndex("dbo.Journal", new[] { "Position_Name" });
            DropTable("dbo.PositionStatusBitsInfo");
            DropTable("dbo.Positions");
            DropTable("dbo.JournalEntryCategories");
            DropTable("dbo.Journal");
        }
    }
}
