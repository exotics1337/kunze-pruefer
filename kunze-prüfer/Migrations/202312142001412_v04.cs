namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v04 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auftrags", "auf_bestell_nr", c => c.Int(nullable: false));
            AlterColumn("dbo.Auftrags", "Auf_prüflos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auftrags", "Auf_prüflos", c => c.String());
            AlterColumn("dbo.Auftrags", "auf_bestell_nr", c => c.String());
        }
    }
}
