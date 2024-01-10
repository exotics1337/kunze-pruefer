namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v01 : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            AddColumn("dbo.Rechnungspositions", "Pruefungstyp_Pe_Typ_nr", c => c.Int(nullable: false));
            AddColumn("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr", c => c.Int());
            CreateIndex("dbo.Rechnungspositions", "Pruefungstyp_Pe_Typ_nr");
            CreateIndex("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr");
            AddForeignKey("dbo.Rechnungspositions", "Pruefungstyp_Pe_Typ_nr", "dbo.Pruefungstyps", "Pe_Typ_nr", cascadeDelete: true);
            AddForeignKey("dbo.Angebotspositions", "Pruefungstyp_Pe_Typ_nr", "dbo.Pruefungstyps", "Pe_Typ_nr");
        }
    }
}
