namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v04 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Probe_Kopf", "P_eingang", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Probe_Kopf", "P_fertigstellung_dat", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Probe_Kopf", "P_abnahme_dat", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Auftrags", "Auf_angenommen", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Auftrags", "Auf_liefertermin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Angebots", "Ang_gueltigkeit_dat", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rechnungs", "r_datum", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rechnungs", "r_zahlungsziel", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rechnungs", "r_angebots_dat", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Rechnungs", "r_pruef_dat", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rechnungs", "r_pruef_dat", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Rechnungs", "r_angebots_dat", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Rechnungs", "r_zahlungsziel", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Rechnungs", "r_datum", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Angebots", "Ang_gueltigkeit_dat", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auftrags", "Auf_liefertermin", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auftrags", "Auf_angenommen", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Probe_Kopf", "P_abnahme_dat", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Probe_Kopf", "P_fertigstellung_dat", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Probe_Kopf", "P_eingang", c => c.DateTime(nullable: false));
        }
    }
}
