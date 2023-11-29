namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Angebotspositions", "Rp_bemerkung", c => c.String());
            AddColumn("dbo.Rechnungspositions", "Rp_bemerkung", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rechnungspositions", "Rp_bemerkung");
            DropColumn("dbo.Angebotspositions", "Rp_bemerkung");
        }
    }
}
