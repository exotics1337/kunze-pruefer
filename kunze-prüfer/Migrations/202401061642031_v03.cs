namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kundes", "k_rech_adresse", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kundes", "k_rech_adresse");
        }
    }
}
