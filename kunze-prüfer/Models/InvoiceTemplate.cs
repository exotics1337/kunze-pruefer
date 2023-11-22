using System;
using System.Collections.Generic;
namespace kunze_prüfer.Models
{
    public class InvoiceTemplate
    {
        public class Adresse
        {
            public string Name { get; set; }
            public string Strasse { get; set; }
            public string Hausnummer { get; set; }
            public string PLZ { get; set; }
            public string Ort { get; set; }
            public string Land { get; set; }
            public string USTID { get; set; }
        }

        public class Artikel
        {
            public string Name { get; set; }
            public double Preis { get; set; }
            public double Menge { get; set; }
        }

        public class AngebotModel
        {
            public class Adresse
            {
                public string Name { get; set; }
                public string Strasse { get; set; }
                public string Hausnummer { get; set; }
                public string PLZ { get; set; }
                public string Ort { get; set; }
                public string Land { get; set; }
                public string USTID { get; set; }
            }

            public class Artikel
            {
                public string Name { get; set; }
                public double Preis { get; set; }
                public double Menge { get; set; }
            }

            public Adresse KundenAdresse { get; set; }
            public List<Artikel> ArtikelList { get; set; }

            public class Info
            {
                public string AngebotNr { get; set; }
                public string Betreff { get; set; }
                public DateTime Datum { get; set; }
            }
        }
    }
}