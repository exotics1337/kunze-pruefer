using System.ComponentModel.DataAnnotations;
using System.Data.Entity;


namespace kunze_prüfer.DataBase
{
    using System;
    using System.Collections.Generic;

    public class KunzeDB : DbContext
    {
        public KunzeDB() : base("KunzeDB") { }

        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Ansprechpartner> Ansprechpartner { get; set; }
        public DbSet<Kunden_Ansprechpartner> Kunden_Ansprechpartner { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Rechnungsposition> Rechnungsposition { get; set; }
        public DbSet<Textbaustein> Textbaustein { get; set; }
        public DbSet<Auftrag> Auftrag { get; set; }
        public DbSet<Werkstoff> Werkstoff { get; set; }
        public DbSet<Rechnung> Rechnung { get; set; }
        public DbSet<Angebot_Textbaustein> Angebot_Textbaustein { get; set; }
        public DbSet<Norm> Norm { get; set; }
        public DbSet<Angebot> Angebot  { get; set; }
        public DbSet<Pruefungstyp> Pruefungstyp { get; set; }
        public DbSet<Abnahmegesellschaft> Abnahmegesellschaft { get; set; }
        public DbSet<Werkstoff_Pruefung> Werkstoff_Pruefung{ get; set; }
        public DbSet<Mehrwertsteuer> Mehrwertsteuer { get; set; }
        public DbSet<Angebotsposition> Angebotsposition { get; set; }
        public DbSet<Probe_Kopf> Probe_Kopf { get; set; }
        public DbSet<Probe_Unter> Probe_Unter { get; set; }
        public DbSet<Fertigstellung_Zeit> Fertigstellung_Zeit { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kunde>()
                .HasKey(k => k.k_nr)
                .HasMany(k => k.Kunden_Ansprechpartner)
                .WithRequired(ka => ka.Kunde)
                .HasForeignKey(ka => ka.K_nr);
                
            
            modelBuilder.Entity<Ansprechpartner>()
                .HasKey(a => a.Anspr_nr);

            modelBuilder.Entity<Kunden_Ansprechpartner>()
                .HasKey(ka => new { ka.K_nr, ka.Anspr_nr })
                .HasMany(a => a.Auftrag)
                .WithRequired(ka => ka.Kunden_Ansprechpartner)
                .HasForeignKey(a => new { a.k_nr, a.Anspr_nr });

            modelBuilder.Entity<Status>()
                .HasKey(s => s.Status_nr);

            modelBuilder.Entity<Rechnungsposition>()
                .HasKey(rp => rp.r_nr);

            modelBuilder.Entity<Textbaustein>()
                .HasKey(t => t.Textbaustein_nr);

            modelBuilder.Entity<Auftrag>()
                .HasKey(a => a.Auf_nr);

            modelBuilder.Entity<Werkstoff>()
                .HasKey(w => w.w_nr); modelBuilder.Entity<Rechnung>()
                .HasKey(r => r.r_nr);

            modelBuilder.Entity<Angebot_Textbaustein>()
                .HasKey(at => new { at.Ang_nr, at.Textbaustein_nr });
            
            modelBuilder.Entity<Norm>()
                .HasKey(n => n.N_nr);
            
            modelBuilder.Entity<Angebot>()
                .HasKey(a => a.Ang_nr);

            modelBuilder.Entity<Pruefungstyp>()
                .HasKey(p => p.Pe_Typ_nr);
            
            modelBuilder.Entity<Abnahmegesellschaft>()
                .HasKey(a => a.Abnahme_nr);

            modelBuilder.Entity<Werkstoff_Pruefung>()
                .HasKey(wp => new { wp.W_nr, wp.Pe_Typ_nr });
            
            modelBuilder.Entity<Mehrwertsteuer>()
                .HasKey(m => m.Mwst_nr);

            modelBuilder.Entity<Angebotsposition>()
                .HasKey(ap => new { ap.Ang_nr, ap.Pe_typ_nr });

            modelBuilder.Entity<Probe_Kopf>()
                .HasKey(pk => pk.P_nr);
            
            modelBuilder.Entity<Probe_Unter>()
                .HasKey(pk => pk.P_nr);
            
            modelBuilder.Entity<Fertigstellung_Zeit>()
                .HasKey(fz => fz.P_fertigstellung_zeit_nr);
            
            modelBuilder.Entity<Mitarbeiter>()
                .HasKey(m => m.M_nr);
            
        }
    }
    
    public class Kunde
    {
        public int k_nr { get; set; }
        public string k_name { get; set; }
        public string k_ust_id { get; set; }
        public string k_lief_adresse { get; set; }
        public bool k_geloescht { get; set; }
        
        //Nav 
        public virtual ICollection<Kunden_Ansprechpartner> Kunden_Ansprechpartner { get; set; }
    }

    public class Ansprechpartner
    {
        public int Anspr_nr { get; set; }
        public string Anspr_vname { get; set; }
        public string Anspr_nname { get; set; }
        public string Anspr_tel { get; set; }
        public string Anspr_email { get; set; }
        public string Anspr_position { get; set; }
        public bool Anspr_geloescht { get; set; }
        
        //Nav
        public virtual ICollection<Kunden_Ansprechpartner> Kunden_Ansprechpartner { get; set; }
    }
    
    public class Kunden_Ansprechpartner
    {
        public int K_nr { get; set; }
        public int Anspr_nr { get; set; }
        
        //Nav 
        public virtual Kunde Kunde { get; set;}
        public virtual Ansprechpartner Ansprechpartner{ get; set;}
        
        public virtual ICollection<Auftrag> Auftrag { get; set; }
    }

    public class Status
    {
        public int Status_nr { get; set; }
        public string Status_bez { get; set; }
        public bool Status_gelöscht { get; set; }
        
       //nav 
        public virtual ICollection<Auftrag> Auftrag { get; set; }
    }

    public class Rechnungsposition
    {
        public int r_nr { get; set; }
        public int Pe_typ_nr { get; set; }
        public double Rp_preis { get; set; }
    }

    public class Textbaustein
    {
        public int Textbaustein_nr { get; set; }
        public string Text_Ueberschrift { get; set; }
        public string Text_Inhalt { get; set; }
        public bool Text_geloescht { get; set; }
    }

    public class Auftrag
    {
        public int Auf_nr { get; set; }
        public DateTime Auf_angenommen { get; set; }
        public DateTime Auf_liefertermin { get; set; }
        public int k_nr { get; set; }
        public int Status_nr { get; set; }
        public int w_nr { get; set; }
        public int n_nr { get; set; }
        public string auf_bestell_nr { get; set; }
        public string Auf_prüflos { get; set; }
        public int Anspr_nr { get; set; }
        public int Prob_nr { get; set; }
        public bool Auf_geloescht { get; set; }
        
        //NAv
        
        public virtual Status Status { get; set;}
        public virtual Kunden_Ansprechpartner Kunden_Ansprechpartner { get; set;}
    }

    public class Werkstoff
    {
        public int w_nr { get; set; }
        public string w_name { get; set; }
        public string w_kurz { get; set; }
        public string w_kennzeichen { get; set; }
        public string w_oberflaeche { get; set; }
        public int w_hoehe { get; set; }
        public int w_breite { get; set; }
        public int w_laenge { get; set; }
        public int w_gewicht { get; set; }
        public bool w_geloescht { get; set; }
    }

    public class Rechnung
    {
        public int r_nr { get; set; }
        public DateTime r_datum { get; set; }
        public DateTime r_zahlungsziel { get; set; }
        public DateTime r_angebots_dat { get; set; }
        public DateTime r_pruef_dat { get; set; }
        public bool r_skontofaehig { get; set; }
        public int Ang_nr { get; set; }
        public bool r_geloescht { get; set; }
    }

    public class Angebot_Textbaustein
    {
        public int Ang_nr { get; set; }
        public int Textbaustein_nr { get; set; }
        
    }

    public class Norm
    {
        public int N_nr { get; set; }
        public string N_bez { get; set; }
        public bool N_geloescht { get; set; }
    }

    public class Angebot
    {
        public int Ang_nr { get; set; }
        public string Ang_probe_vorraussetzung { get; set; }
        public bool Ang_angenommen { get; set; }
        public DateTime Ang_gueltigkeit_dat { get; set; }
        public int Mwst_nr { get; set; }
        public int Auf_nr { get; set; }
        public bool Ang_geloescht { get; set; }
    }

    public class Pruefungstyp
    {
        public int Pe_Typ_nr { get; set; }
        public string Pe_typ_bez { get; set; }
        public decimal Pe_durch_preis { get; set; }
        public bool Pe_geloescht { get; set; }
    }

    public class Abnahmegesellschaft
    {
        public int Abnahme_nr { get; set; }
        public string Abnhme_bez { get; set; }
        public bool Abnahme_geloescht { get; set; }
    }

    public class Werkstoff_Pruefung
    {
        public int W_nr { get; set; }
        public int Pe_Typ_nr { get; set; }
    }

    public class Mehrwertsteuer
    {
        public int Mwst_nr { get; set; }
        public int Mwst_satz { get; set; }
        public bool Mwst_geloescht { get; set; }
    }
    
    public class Angebotsposition
    {
        public int Ang_nr { get; set; }
        public int Pe_typ_nr { get; set; }
        public double Rp_preis { get; set; }
    }

    public class Probe_Kopf
    {
        public int P_nr { get; set; }
        public int Prob_nr { get; set; }
        public DateTime P_eingang { get; set; }
        public DateTime P_fertigstellung_dat { get; set; }
        public int P_fertigstellung_zeit_nr { get; set; }
        public DateTime P_abnahme_dat { get; set; }
        public string P_charge_nr { get; set; }
        public string P_bemerkung { get; set; }
        public string P_sonstige1 { get; set; }
        public string P_sonstige2 { get; set; }
        public string P_sonstige3 { get; set; }
        public int P_anzahl { get; set; }
        public bool P_abgeschlossen { get; set; }
        public int Abnahme_nr { get; set; }
        public bool P_kopf_geloescht { get; set; }
    }

    public class Probe_Unter
    {
        public int P_nr { get; set; }
        public int Pe_nr { get; set; }
        public int Pe_typ_nr { get; set; }
        public int Pe_anzahl { get; set; }
        public string Pe_temp { get; set; }
        public string Pe_probenform { get; set; }
        public string Pe_probenlage { get; set; }
        public string Pe_norm { get; set; }
        public string Pe_bemerkung { get; set; }
        public int M_nr { get; set; }
        public string P_ergebnis_text { get; set; }
        public bool P_unter_geloescht { get; set; }
    }

    public class Fertigstellung_Zeit
    {
        public int P_fertigstellung_zeit_nr { get; set; }
        public string P_fertigstellung_zeit_bez { get; set; }
        public bool P_fertigstellung_geloescht { get; set; }
    }

    public class Mitarbeiter
    {
        public int M_nr { get; set; }
        public string M_vname { get; set; }
        public string M_nname { get; set; }
        public string M_pass { get; set; }
        public bool M_admin { get; set; }
        public bool M_geloescht { get; set; }
    }

}