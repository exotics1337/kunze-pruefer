namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reltest : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Kunden_Ansprechpartner", "K_nr");
            CreateIndex("dbo.Kunden_Ansprechpartner", "Anspr_nr");
            AddForeignKey("dbo.Kunden_Ansprechpartner", "Anspr_nr", "dbo.Ansprechpartners", "Anspr_nr", cascadeDelete: true);
            AddForeignKey("dbo.Kunden_Ansprechpartner", "K_nr", "dbo.Kundes", "k_nr", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kunden_Ansprechpartner", "K_nr", "dbo.Kundes");
            DropForeignKey("dbo.Kunden_Ansprechpartner", "Anspr_nr", "dbo.Ansprechpartners");
            DropIndex("dbo.Kunden_Ansprechpartner", new[] { "Anspr_nr" });
            DropIndex("dbo.Kunden_Ansprechpartner", new[] { "K_nr" });
        }
    }
}
