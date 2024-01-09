namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v06 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Angebotspositions", "Pe_typ_nr", "dbo.Pruefungstyps");
            DropIndex("dbo.Angebotspositions", new[] { "Pe_typ_nr" });
            RenameColumn(table: "dbo.Angebotspositions", name: "Pe_typ_nr", newName: "Pruefungstyp_Pe_Typ_nr");
            RenameColumn(table: "dbo.Rechnungspositions", name: "Pe_typ_nr", newName: "Pruefungstyp_Pe_Typ_nr");
            RenameIndex(table: "dbo.Rechnungspositions", name: "IX_Pe_typ_nr", newName: "IX_Pruefungstyp_Pe_Typ_nr");
            DropPrimaryKey("dbo.Angebotspositions");
            DropPrimaryKey("dbo.Rechnungspositions");
            AddColumn("dbo.Angebotspositions", "Ang2_nr", c => c.Int(nullable: false));
            AddColumn("dbo.Angebotspositions", "Rp_name", c => c.String());
            AddColumn("dbo.Rechnungspositions", "r2_nr", c => c.Int(nullable: false));
            AddColumn("dbo.Rechnungspositions", "Rp_name", c => c.String());
            AlterColumn("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr", c => c.Int());
            AddPrimaryKey("dbo.Angebotspositions", new[] { "Ang_nr", "Ang2_nr" });
            AddPrimaryKey("dbo.Rechnungspositions", new[] { "r_nr", "r2_nr" });
            CreateIndex("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr");
            AddForeignKey("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr", "dbo.Pruefungstyps", "Pe_Typ_nr");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr", "dbo.Pruefungstyps");
            DropIndex("dbo.Angebotspositions", new[] { "Pruefungstyp_Pe_Typ_nr" });
            DropPrimaryKey("dbo.Rechnungspositions");
            DropPrimaryKey("dbo.Angebotspositions");
            AlterColumn("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr", c => c.Int(nullable: false));
            DropColumn("dbo.Rechnungspositions", "Rp_name");
            DropColumn("dbo.Rechnungspositions", "r2_nr");
            DropColumn("dbo.Angebotspositions", "Rp_name");
            DropColumn("dbo.Angebotspositions", "Ang2_nr");
            AddPrimaryKey("dbo.Rechnungspositions", new[] { "r_nr", "Pe_typ_nr" });
            AddPrimaryKey("dbo.Angebotspositions", new[] { "Ang_nr", "Pe_typ_nr" });
            RenameIndex(table: "dbo.Rechnungspositions", name: "IX_Pruefungstyp_Pe_Typ_nr", newName: "IX_Pe_typ_nr");
            RenameColumn(table: "dbo.Rechnungspositions", name: "Pruefungstyp_Pe_Typ_nr", newName: "Pe_typ_nr");
            RenameColumn(table: "dbo.Angebotspositions", name: "Pruefungstyp_Pe_Typ_nr", newName: "Pe_typ_nr");
            CreateIndex("dbo.Angebotspositions", "Pe_typ_nr");
            AddForeignKey("dbo.Angebotspositions", "Pe_typ_nr", "dbo.Pruefungstyps", "Pe_Typ_nr", cascadeDelete: true);
        }
    }
}
