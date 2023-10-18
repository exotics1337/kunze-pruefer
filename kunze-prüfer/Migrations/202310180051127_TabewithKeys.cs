namespace kunze_prüfer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabewithKeys : DbMigration
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
                .PrimaryKey(t => t.Ang_nr);
            
            CreateTable(
                "dbo.Angebot_Textbaustein",
                c => new
                    {
                        Ang_nr = c.Int(nullable: false),
                        Textbaustein_nr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ang_nr, t.Textbaustein_nr });
            
            CreateTable(
                "dbo.Angebotspositions",
                c => new
                    {
                        Ang_nr = c.Int(nullable: false),
                        Pe_typ_nr = c.Int(nullable: false),
                        Rp_preis = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ang_nr, t.Pe_typ_nr });
            
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
                .PrimaryKey(t => t.Auf_nr);
            
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
                "dbo.Kunden_Ansprechpartner",
                c => new
                    {
                        K_nr = c.Int(nullable: false),
                        Anspr_nr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.K_nr, t.Anspr_nr });
            
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
                .PrimaryKey(t => t.P_nr);
            
            CreateTable(
                "dbo.Probe_Unter",
                c => new
                    {
                        P_nr = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.P_nr);
            
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
                .PrimaryKey(t => t.r_nr);
            
            CreateTable(
                "dbo.Rechnungspositions",
                c => new
                    {
                        r_nr = c.Int(nullable: false, identity: true),
                        Pe_typ_nr = c.Int(nullable: false),
                        Rp_preis = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.r_nr);
            
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
                "dbo.Werkstoff_Pruefung",
                c => new
                    {
                        W_nr = c.Int(nullable: false),
                        Pe_Typ_nr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.W_nr, t.Pe_Typ_nr });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Werkstoff_Pruefung");
            DropTable("dbo.Werkstoffs");
            DropTable("dbo.Textbausteins");
            DropTable("dbo.Status");
            DropTable("dbo.Rechnungspositions");
            DropTable("dbo.Rechnungs");
            DropTable("dbo.Pruefungstyps");
            DropTable("dbo.Probe_Unter");
            DropTable("dbo.Probe_Kopf");
            DropTable("dbo.Norms");
            DropTable("dbo.Mitarbeiters");
            DropTable("dbo.Mehrwertsteuers");
            DropTable("dbo.Kunden_Ansprechpartner");
            DropTable("dbo.Kundes");
            DropTable("dbo.Fertigstellung_Zeit");
            DropTable("dbo.Auftrags");
            DropTable("dbo.Ansprechpartners");
            DropTable("dbo.Angebotspositions");
            DropTable("dbo.Angebot_Textbaustein");
            DropTable("dbo.Angebots");
            DropTable("dbo.Abnahmegesellschafts");
        }
    }
}
