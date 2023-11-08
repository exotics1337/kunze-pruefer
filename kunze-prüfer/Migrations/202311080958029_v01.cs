namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abnahmegesellschafts",
                c => new
                    {
                        Abnahme_nr = c.Int(nullable: false, identity: true),
                        Abnhme_bez = c.String(),
                        Abnahme_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Abnahme_nr);
            
            CreateTable(
                "dbo.Probe_Kopf",
                c => new
                    {
                        P_nr = c.Int(nullable: false, identity: true),
                        Prob_nr = c.Int(nullable: false),
                        P_eingang = c.DateTime(nullable: false),
                        P_fertigstellung_dat = c.DateTime(nullable: false),
                        P_fertigstellung_zeit_nr = c.Int(nullable: false),
                        P_abnahme_dat = c.DateTime(nullable: false),
                        P_charge_nr = c.String(),
                        P_bemerkung = c.String(),
                        P_sonstige1 = c.String(),
                        P_sonstige2 = c.String(),
                        P_sonstige3 = c.String(),
                        P_anzahl = c.Int(nullable: false),
                        P_abgeschlossen = c.Boolean(nullable: false),
                        Abnahme_nr = c.Int(nullable: false),
                        P_kopf_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.P_nr)
                .ForeignKey("dbo.Auftrags", t => t.Prob_nr, cascadeDelete: true)
                .ForeignKey("dbo.Fertigstellung_Zeit", t => t.P_fertigstellung_zeit_nr, cascadeDelete: true)
                .ForeignKey("dbo.Abnahmegesellschafts", t => t.Abnahme_nr, cascadeDelete: true)
                .Index(t => t.Prob_nr)
                .Index(t => t.P_fertigstellung_zeit_nr)
                .Index(t => t.Abnahme_nr);
            
            CreateTable(
                "dbo.Auftrags",
                c => new
                    {
                        Auf_nr = c.Int(nullable: false, identity: true),
                        Auf_angenommen = c.DateTime(nullable: false),
                        Auf_liefertermin = c.DateTime(nullable: false),
                        k_nr = c.Int(nullable: false),
                        Status_nr = c.Int(nullable: false),
                        w_nr = c.Int(nullable: false),
                        n_nr = c.Int(nullable: false),
                        auf_bestell_nr = c.String(),
                        Auf_prüflos = c.String(),
                        Anspr_nr = c.Int(nullable: false),
                        Prob_nr = c.Int(nullable: false),
                        Auf_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Auf_nr)
                .ForeignKey("dbo.Kunden_Ansprechpartner", t => new { t.k_nr, t.Anspr_nr }, cascadeDelete: true)
                .ForeignKey("dbo.Norms", t => t.n_nr, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_nr, cascadeDelete: true)
                .ForeignKey("dbo.Werkstoffs", t => t.w_nr, cascadeDelete: true)
                .Index(t => new { t.k_nr, t.Anspr_nr })
                .Index(t => t.Status_nr)
                .Index(t => t.w_nr)
                .Index(t => t.n_nr);
            
            CreateTable(
                "dbo.Angebots",
                c => new
                    {
                        Ang_nr = c.Int(nullable: false, identity: true),
                        Ang_probe_vorraussetzung = c.String(),
                        Ang_angenommen = c.Boolean(nullable: false),
                        Ang_gueltigkeit_dat = c.DateTime(nullable: false),
                        Mwst_nr = c.Int(nullable: false),
                        Auf_nr = c.Int(nullable: false),
                        Ang_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Ang_nr)
                .ForeignKey("dbo.Auftrags", t => t.Auf_nr, cascadeDelete: true)
                .ForeignKey("dbo.Mehrwertsteuers", t => t.Mwst_nr, cascadeDelete: true)
                .Index(t => t.Mwst_nr)
                .Index(t => t.Auf_nr);
            
            CreateTable(
                "dbo.Angebotspositions",
                c => new
                    {
                        Ang_nr = c.Int(nullable: false),
                        Pe_typ_nr = c.Int(nullable: false),
                        Rp_preis = c.Double(nullable: false),
                        Rp_menge = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ang_nr, t.Pe_typ_nr })
                .ForeignKey("dbo.Pruefungstyps", t => t.Pe_typ_nr, cascadeDelete: true)
                .ForeignKey("dbo.Angebots", t => t.Ang_nr, cascadeDelete: true)
                .Index(t => t.Ang_nr)
                .Index(t => t.Pe_typ_nr);
            
            CreateTable(
                "dbo.Pruefungstyps",
                c => new
                    {
                        Pe_Typ_nr = c.Int(nullable: false, identity: true),
                        Pe_typ_bez = c.String(),
                        Pe_durch_preis = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pe_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Pe_Typ_nr);
            
            CreateTable(
                "dbo.Rechnungspositions",
                c => new
                    {
                        r_nr = c.Int(nullable: false),
                        Pe_typ_nr = c.Int(nullable: false),
                        Rp_preis = c.Double(nullable: false),
                        Rp_menge = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.r_nr, t.Pe_typ_nr })
                .ForeignKey("dbo.Rechnungs", t => t.r_nr, cascadeDelete: true)
                .ForeignKey("dbo.Pruefungstyps", t => t.Pe_typ_nr, cascadeDelete: true)
                .Index(t => t.r_nr)
                .Index(t => t.Pe_typ_nr);
            
            CreateTable(
                "dbo.Rechnungs",
                c => new
                    {
                        r_nr = c.Int(nullable: false, identity: true),
                        r_datum = c.DateTime(nullable: false),
                        r_zahlungsziel = c.DateTime(nullable: false),
                        r_angebots_dat = c.DateTime(nullable: false),
                        r_pruef_dat = c.DateTime(nullable: false),
                        r_skontofaehig = c.Boolean(nullable: false),
                        Ang_nr = c.Int(nullable: false),
                        r_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.r_nr)
                .ForeignKey("dbo.Angebots", t => t.Ang_nr, cascadeDelete: true)
                .Index(t => t.Ang_nr);
            
            CreateTable(
                "dbo.Werkstoff_Pruefung",
                c => new
                    {
                        W_nr = c.Int(nullable: false),
                        Pe_Typ_nr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.W_nr, t.Pe_Typ_nr })
                .ForeignKey("dbo.Werkstoffs", t => t.W_nr, cascadeDelete: true)
                .ForeignKey("dbo.Pruefungstyps", t => t.Pe_Typ_nr, cascadeDelete: true)
                .Index(t => t.W_nr)
                .Index(t => t.Pe_Typ_nr);
            
            CreateTable(
                "dbo.Werkstoffs",
                c => new
                    {
                        w_nr = c.Int(nullable: false, identity: true),
                        w_name = c.String(),
                        w_kurz = c.String(),
                        w_kennzeichen = c.String(),
                        w_oberflaeche = c.String(),
                        w_hoehe = c.Int(nullable: false),
                        w_breite = c.Int(nullable: false),
                        w_laenge = c.Int(nullable: false),
                        w_gewicht = c.Int(nullable: false),
                        w_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.w_nr);
            
            CreateTable(
                "dbo.Angebot_Textbaustein",
                c => new
                    {
                        Ang_nr = c.Int(nullable: false),
                        Textbaustein_nr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ang_nr, t.Textbaustein_nr })
                .ForeignKey("dbo.Textbausteins", t => t.Textbaustein_nr, cascadeDelete: true)
                .ForeignKey("dbo.Angebots", t => t.Ang_nr, cascadeDelete: true)
                .Index(t => t.Ang_nr)
                .Index(t => t.Textbaustein_nr);
            
            CreateTable(
                "dbo.Textbausteins",
                c => new
                    {
                        Textbaustein_nr = c.Int(nullable: false, identity: true),
                        Text_Ueberschrift = c.String(),
                        Text_Inhalt = c.String(),
                        Text_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Textbaustein_nr);
            
            CreateTable(
                "dbo.Mehrwertsteuers",
                c => new
                    {
                        Mwst_nr = c.Int(nullable: false, identity: true),
                        Mwst_satz = c.Int(nullable: false),
                        Mwst_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Mwst_nr);
            
            CreateTable(
                "dbo.Kunden_Ansprechpartner",
                c => new
                    {
                        K_nr = c.Int(nullable: false),
                        Anspr_nr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.K_nr, t.Anspr_nr })
                .ForeignKey("dbo.Ansprechpartners", t => t.Anspr_nr, cascadeDelete: true)
                .ForeignKey("dbo.Kundes", t => t.K_nr, cascadeDelete: true)
                .Index(t => t.K_nr)
                .Index(t => t.Anspr_nr);
            
            CreateTable(
                "dbo.Ansprechpartners",
                c => new
                    {
                        Anspr_nr = c.Int(nullable: false, identity: true),
                        Anspr_vname = c.String(),
                        Anspr_nname = c.String(),
                        Anspr_tel = c.String(),
                        Anspr_email = c.String(),
                        Anspr_position = c.String(),
                        Anspr_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Anspr_nr);
            
            CreateTable(
                "dbo.Kundes",
                c => new
                    {
                        k_nr = c.Int(nullable: false, identity: true),
                        k_name = c.String(),
                        k_ust_id = c.String(),
                        k_lief_adresse = c.String(),
                        k_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.k_nr);
            
            CreateTable(
                "dbo.Norms",
                c => new
                    {
                        N_nr = c.Int(nullable: false, identity: true),
                        N_bez = c.String(),
                        N_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.N_nr);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Status_nr = c.Int(nullable: false, identity: true),
                        Status_bez = c.String(),
                        Status_gelöscht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Status_nr);
            
            CreateTable(
                "dbo.Fertigstellung_Zeit",
                c => new
                    {
                        P_fertigstellung_zeit_nr = c.Int(nullable: false, identity: true),
                        P_fertigstellung_zeit_bez = c.String(),
                        P_fertigstellung_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.P_fertigstellung_zeit_nr);
            
            CreateTable(
                "dbo.Probe_Unter",
                c => new
                    {
                        P_nr = c.Int(nullable: false),
                        Pe_nr = c.Int(nullable: false),
                        Pe_typ_nr = c.Int(nullable: false),
                        Pe_anzahl = c.Int(nullable: false),
                        Pe_temp = c.String(),
                        Pe_probenform = c.String(),
                        Pe_probenlage = c.String(),
                        Pe_norm = c.String(),
                        Pe_bemerkung = c.String(),
                        M_nr = c.Int(nullable: false),
                        P_ergebnis_text = c.String(),
                        P_unter_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.P_nr, t.Pe_nr })
                .ForeignKey("dbo.Mitarbeiters", t => t.M_nr, cascadeDelete: true)
                .ForeignKey("dbo.Probe_Kopf", t => t.P_nr, cascadeDelete: true)
                .Index(t => t.P_nr)
                .Index(t => t.M_nr);
            
            CreateTable(
                "dbo.Mitarbeiters",
                c => new
                    {
                        M_nr = c.Int(nullable: false, identity: true),
                        M_vname = c.String(),
                        M_nname = c.String(),
                        M_pass = c.String(),
                        M_admin = c.Boolean(nullable: false),
                        M_geloescht = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.M_nr);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Probe_Kopf", "Abnahme_nr", "dbo.Abnahmegesellschafts");
            DropForeignKey("dbo.Probe_Unter", "P_nr", "dbo.Probe_Kopf");
            DropForeignKey("dbo.Probe_Unter", "M_nr", "dbo.Mitarbeiters");
            DropForeignKey("dbo.Probe_Kopf", "P_fertigstellung_zeit_nr", "dbo.Fertigstellung_Zeit");
            DropForeignKey("dbo.Auftrags", "w_nr", "dbo.Werkstoffs");
            DropForeignKey("dbo.Auftrags", "Status_nr", "dbo.Status");
            DropForeignKey("dbo.Probe_Kopf", "Prob_nr", "dbo.Auftrags");
            DropForeignKey("dbo.Auftrags", "n_nr", "dbo.Norms");
            DropForeignKey("dbo.Auftrags", new[] { "k_nr", "Anspr_nr" }, "dbo.Kunden_Ansprechpartner");
            DropForeignKey("dbo.Kunden_Ansprechpartner", "K_nr", "dbo.Kundes");
            DropForeignKey("dbo.Kunden_Ansprechpartner", "Anspr_nr", "dbo.Ansprechpartners");
            DropForeignKey("dbo.Rechnungs", "Ang_nr", "dbo.Angebots");
            DropForeignKey("dbo.Angebots", "Mwst_nr", "dbo.Mehrwertsteuers");
            DropForeignKey("dbo.Angebots", "Auf_nr", "dbo.Auftrags");
            DropForeignKey("dbo.Angebot_Textbaustein", "Ang_nr", "dbo.Angebots");
            DropForeignKey("dbo.Angebot_Textbaustein", "Textbaustein_nr", "dbo.Textbausteins");
            DropForeignKey("dbo.Angebotspositions", "Ang_nr", "dbo.Angebots");
            DropForeignKey("dbo.Angebotspositions", "Pe_typ_nr", "dbo.Pruefungstyps");
            DropForeignKey("dbo.Werkstoff_Pruefung", "Pe_Typ_nr", "dbo.Pruefungstyps");
            DropForeignKey("dbo.Werkstoff_Pruefung", "W_nr", "dbo.Werkstoffs");
            DropForeignKey("dbo.Rechnungspositions", "Pe_typ_nr", "dbo.Pruefungstyps");
            DropForeignKey("dbo.Rechnungspositions", "r_nr", "dbo.Rechnungs");
            DropIndex("dbo.Probe_Unter", new[] { "M_nr" });
            DropIndex("dbo.Probe_Unter", new[] { "P_nr" });
            DropIndex("dbo.Kunden_Ansprechpartner", new[] { "Anspr_nr" });
            DropIndex("dbo.Kunden_Ansprechpartner", new[] { "K_nr" });
            DropIndex("dbo.Angebot_Textbaustein", new[] { "Textbaustein_nr" });
            DropIndex("dbo.Angebot_Textbaustein", new[] { "Ang_nr" });
            DropIndex("dbo.Werkstoff_Pruefung", new[] { "Pe_Typ_nr" });
            DropIndex("dbo.Werkstoff_Pruefung", new[] { "W_nr" });
            DropIndex("dbo.Rechnungs", new[] { "Ang_nr" });
            DropIndex("dbo.Rechnungspositions", new[] { "Pe_typ_nr" });
            DropIndex("dbo.Rechnungspositions", new[] { "r_nr" });
            DropIndex("dbo.Angebotspositions", new[] { "Pe_typ_nr" });
            DropIndex("dbo.Angebotspositions", new[] { "Ang_nr" });
            DropIndex("dbo.Angebots", new[] { "Auf_nr" });
            DropIndex("dbo.Angebots", new[] { "Mwst_nr" });
            DropIndex("dbo.Auftrags", new[] { "n_nr" });
            DropIndex("dbo.Auftrags", new[] { "w_nr" });
            DropIndex("dbo.Auftrags", new[] { "Status_nr" });
            DropIndex("dbo.Auftrags", new[] { "k_nr", "Anspr_nr" });
            DropIndex("dbo.Probe_Kopf", new[] { "Abnahme_nr" });
            DropIndex("dbo.Probe_Kopf", new[] { "P_fertigstellung_zeit_nr" });
            DropIndex("dbo.Probe_Kopf", new[] { "Prob_nr" });
            DropTable("dbo.Mitarbeiters");
            DropTable("dbo.Probe_Unter");
            DropTable("dbo.Fertigstellung_Zeit");
            DropTable("dbo.Status");
            DropTable("dbo.Norms");
            DropTable("dbo.Kundes");
            DropTable("dbo.Ansprechpartners");
            DropTable("dbo.Kunden_Ansprechpartner");
            DropTable("dbo.Mehrwertsteuers");
            DropTable("dbo.Textbausteins");
            DropTable("dbo.Angebot_Textbaustein");
            DropTable("dbo.Werkstoffs");
            DropTable("dbo.Werkstoff_Pruefung");
            DropTable("dbo.Rechnungs");
            DropTable("dbo.Rechnungspositions");
            DropTable("dbo.Pruefungstyps");
            DropTable("dbo.Angebotspositions");
            DropTable("dbo.Angebots");
            DropTable("dbo.Auftrags");
            DropTable("dbo.Probe_Kopf");
            DropTable("dbo.Abnahmegesellschafts");
        }
    }
}
