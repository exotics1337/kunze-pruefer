using System;
using System.Collections.Generic;
namespace kunze_prüfer.Models
{
    public class InvoiceTemplate
    {
        public class InvoiceKunde
        {
            public string Name { get; set; }
            public string Adresse { get; set; }
            public string USTID { get; set; }
        }

        public class Artikel
        {
            public string Name { get; set; }
            public double Preis { get; set; }
            public double Menge { get; set; }
            public string Bemerkung { get; set; }
        }
        
        public class Kommentar
        {
            public string Header { get; set; }
            public string Text { get; set; }
        }

        public class AngebotModel
        {
            public InvoiceKunde Kunde { get; set; }
            public List<Artikel> ArtikelList { get; set; }
            public List<Kommentar> KommentarList { get; set; }
            
            public int AngebotNr { get; set; }
            public double MwSt { get; set; }
        }
        
        public class RechnungModel
        {
            public InvoiceKunde Kunde { get; set; }
            public List<Artikel> ArtikelList { get; set; }
            public List<Kommentar> KommentarList { get; set; }
            public int RechnungNr { get; set; }
        }
    }
}